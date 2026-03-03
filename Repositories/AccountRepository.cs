using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Dtos.Account;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;

namespace PetSavingBackend.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;    
        public AccountRepository(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id.ToString());
            if (userToDelete == null)
            {
                return IdentityResult.Failed(new IdentityError {Description = $"No se encontro un usuario con el ID {id}"});
            }
            return await _userManager.DeleteAsync(userToDelete);
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(Guid id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> PatchAccountAsync(Guid id, PatchAccountRequestDTO updateDTO)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"No se encontro un usuario con el ID {id}"});
            }

            if (!string.IsNullOrEmpty(updateDTO.UserName))
            {
                existingUser.UserName = updateDTO.UserName;
            }
            if (!string.IsNullOrEmpty(updateDTO.Email))
            {
                existingUser.Email = updateDTO.Email;
            }
            if (!string.IsNullOrEmpty(updateDTO.PhoneNumber))
            {
                existingUser.PhoneNumber = updateDTO.PhoneNumber;
            }
            if (!string.IsNullOrEmpty(updateDTO.Specialization))
            {
                existingUser.Specialization = updateDTO.Specialization;
            }
            if (updateDTO.BirthDate.HasValue)
            {
                existingUser.BirthDate = updateDTO.BirthDate.Value;
            }
             if (!string.IsNullOrEmpty(updateDTO.Activity))
            {
                existingUser.Activity = updateDTO.Activity;
            }
            if (!string.IsNullOrEmpty(updateDTO.Password))
            {
                existingUser.PasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, updateDTO.Password);
            }

            return await _userManager.UpdateAsync(existingUser);
        }

        public async Task<RegisterAndLoginRequestDTO?> RegisterNewUserAsync(AppUser registerNew, string role, string password)
        {
            var createUser = await _userManager.CreateAsync(registerNew, password);
            if (createUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(registerNew, role);
                if (roleResult.Succeeded)
                {
                    return new RegisterAndLoginRequestDTO
                    {
                        UserName = registerNew.UserName,
                        Token = await _tokenService.CreateTokenAsync(registerNew)
                    };

                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<AppUser?> UserExistCheckByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<AppUser?> UserExistCheckByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}