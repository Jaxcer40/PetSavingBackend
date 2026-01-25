using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.Client;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
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
            .Select(s=>s.ToReadClientDto()).ToListAsync();

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

            return Ok(client.ToReadClientDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto clientDto)
        {
            // Validar que el DTO no sea nulo
            if (clientDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var clientModel = clientDto.ToClientFromCreateDto();
            await _context.Clients.AddAsync(clientModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id=clientModel.Id}, clientModel.ToReadClientDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]UpdateClientDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var clientModel= await _context.Clients.FirstOrDefaultAsync(x=>x.Id==id);

            if (clientModel == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrWhiteSpace(updateDto.FirstName))
                clientModel.FirstName=updateDto.FirstName;
            
            if(!string.IsNullOrWhiteSpace(updateDto.LastName))
                clientModel.LastName=updateDto.LastName;
            
            if(!string.IsNullOrWhiteSpace(updateDto.Email))
                clientModel.Email=updateDto.Email;

            if(!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
                clientModel.PhoneNumber=updateDto.PhoneNumber;

            if(!string.IsNullOrWhiteSpace(updateDto.Address))
                clientModel.Address=updateDto.Address;
            
            if(updateDto.BirthDate.HasValue)
                clientModel.BirthDate=updateDto.BirthDate.Value;
            
            if(updateDto.RegistrationDate.HasValue)
                clientModel.RegistrationDate=updateDto.RegistrationDate.Value;

            if(!string.IsNullOrWhiteSpace(updateDto.EmergencyContactName))
                clientModel.EmergencyContactName=updateDto.EmergencyContactName;
            
            if(!string.IsNullOrWhiteSpace(updateDto.EmergencyContactPhone))
                clientModel.EmergencyContactPhone=updateDto.EmergencyContactPhone;
            
            await _context.SaveChangesAsync();

            return Ok(clientModel.ToReadClientDto());
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