using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Data;
using Microsoft.AspNetCore.Mvc;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Client;
using System.Data;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;

namespace PetSavingBackend.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepo;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepo=clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients= await _clientRepo.GetAllAsync();
            return Ok(clients.Select(c=>c.ToReadClientDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var client= await _clientRepo.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client.ToReadClientDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDTO clientDTO)
        {
            // Validar que el DTO no sea nulo
            if (clientDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var clientModel = clientDTO.ToClientFromCreateDTO();
            await _clientRepo.CreateAsync(clientModel);
            return CreatedAtAction(nameof(GetById), new {id=clientModel.Id}, clientModel.ToReadClientDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]UpdateClientDTO updateDTO)
        {
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var updateClient= await _clientRepo.PatchAsync(id, updateDTO);

            if (updateClient == null)
            {
                return NotFound();
            }

            return Ok(updateClient.ToReadClientDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var clientModel= await _clientRepo.DeleteAsync(id);

            if (clientModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}