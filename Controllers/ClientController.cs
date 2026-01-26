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

namespace PetSavingBackend.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ClientController(ApplicationDBContext context)
        {
            _context= context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients= await _context.Clients
            .Select(s=>s.ToReadClientDTO()).ToListAsync();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var client= await _context.Clients.FindAsync(id);

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
            await _context.Clients.AddAsync(clientModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id=clientModel.Id}, clientModel.ToReadClientDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]UpdateClientDTO updateDTO)
        {
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var clientModel= await _context.Clients.FirstOrDefaultAsync(x=>x.Id==id);

            if (clientModel == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrWhiteSpace(updateDTO.FirstName))
                clientModel.FirstName=updateDTO.FirstName;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.LastName))
                clientModel.LastName=updateDTO.LastName;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Email))
                clientModel.Email=updateDTO.Email;

            if(!string.IsNullOrWhiteSpace(updateDTO.PhoneNumber))
                clientModel.PhoneNumber=updateDTO.PhoneNumber;

            if(!string.IsNullOrWhiteSpace(updateDTO.Address))
                clientModel.Address=updateDTO.Address;
            
            if(updateDTO.BirthDate.HasValue)
                clientModel.BirthDate=updateDTO.BirthDate.Value;
            
            if(updateDTO.RegistrationDate.HasValue)
                clientModel.RegistrationDate=updateDTO.RegistrationDate.Value;

            if(!string.IsNullOrWhiteSpace(updateDTO.EmergencyContactName))
                clientModel.EmergencyContactName=updateDTO.EmergencyContactName;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.EmergencyContactPhone))
                clientModel.EmergencyContactPhone=updateDTO.EmergencyContactPhone;
            
            await _context.SaveChangesAsync();

            return Ok(clientModel.ToReadClientDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var clientModel= await _context.Clients.FirstOrDefaultAsync(x=>x.Id==id);
            if (clientModel == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clientModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}