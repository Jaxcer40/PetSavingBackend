using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PetSavingBackend.Dtos.Account;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Mappers;
using PetSavingBackend.Models;

namespace PetSavingBackend.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly  ITokenService _tokenService;
        private readonly SignInManager<AppUser> _SignInManager;

        public AccountController (IAccountRepository accountRepo, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _accountRepo = accountRepo;
            _tokenService = tokenService;
            _SignInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PostAccountRequestDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var ExistingUser = await _accountRepo.UserExistCheckByEmailAsync(registerDTO.Email);
                if (ExistingUser != null)
                {
                    return BadRequest(new { message = "Ya existe un usuario registrado con ese correo electronico"});
                }
                var newUser = new AppUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    BirthDate = registerDTO.BirthDate,
                    Specialization = registerDTO.Specialization,
                    Activity = registerDTO.Activity
                };
                var result = await _accountRepo.RegisterNewUserAsync(newUser, "Veterinario", registerDTO.Password);
                if (result == null)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al registrar el usuario", error = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO LoginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _accountRepo.UserExistCheckByEmailAsync(LoginDTO.Email);
                if (user == null)
                {
                    return Unauthorized(new {message = "Credenciales invalidas."});
                }
                var signIngResult = await _SignInManager.CheckPasswordSignInAsync(user, LoginDTO.Password, false);
                if (!signIngResult.Succeeded)
                {
                    return Unauthorized(new { message = "Credenciales invalidas."});
                }
                return Ok(new LoginResponseDTO
                {
                    UserName = user.UserName,
                    Token = await _tokenService.CreateTokenAsync(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message ="Ocurrio un error al iniciar sesion", error = ex.Message});
            }

        }
        [HttpGet]
        public async Task<IActionResult> GettAll()
        {
            var users = await _accountRepo.GetAllAsync();
            return Ok(users.Select(u => u.ToGETAccountRequestDTOFromAppUser()));
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var user = await _accountRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"No se encontro un usuario con el ID{id}."});
            }
            return Ok(user.ToGETAccountRequestDTOFromAppUser());
        }
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PatchAccount([FromRoute] Guid id, [FromBody] PatchAccountRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _accountRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new {message = $"No se encontro un usuario con el ID{id}."});
            }
            var result = await _accountRepo.PatchAccountAsync(id, updateDTO);
            if (result.Succeeded)
            {
                var user = await _accountRepo.GetByIdAsync(id);
                return Ok(user.ToGETAccountRequestDTOFromAppUser());
            }
            else
            {
                return StatusCode(500, result.Errors);
            }
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _accountRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("No se encontro un usuario con el ID proporcionado");
            }
            var result = await _accountRepo.DeleteAsync(id);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, result.Errors);
            }
        }
    }

}