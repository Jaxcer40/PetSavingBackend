using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Vet;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;

namespace PetSavingBackend.Controllers
{
    [Route("api/vet")]
    [ApiController]
    public class VetController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public VetController(ApplicationDBContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vets= await _context.Vets
            .Select(s=>s.ToReadVetDTO()).ToListAsync();
           
            return Ok(vets);
        }        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var vet= await _context.Vets.FindAsync(id);

            if (vet == null)
            {
                return NotFound();
            }

            return Ok(vet.ToReadVetDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVetDTO vetDTO)
        {
            // Validar que el DTO no sea nulo
            if (vetDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var vetModel= vetDTO.ToVetFromCreateDTO();
            await _context.Vets.AddAsync(vetModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id=vetModel.Id}, vetModel.ToReadVetDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateVetDTO updateVet)
        {
            if (updateVet == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var vetModel= await _context.Vets.FirstOrDefaultAsync(x=>x.Id==id);

            if (vetModel == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrWhiteSpace(updateVet.FirstName))
                vetModel.FirstName= updateVet.FirstName;
            
            if(!string.IsNullOrWhiteSpace(updateVet.LastName))
                vetModel.LastName= updateVet.LastName;
            
            if(!string.IsNullOrWhiteSpace(updateVet.Email))
                vetModel.Email= updateVet.Email;
            
            if(!string.IsNullOrWhiteSpace(updateVet.PhoneNumber))
                vetModel.PhoneNumber= updateVet.PhoneNumber;
            
            if(!string.IsNullOrWhiteSpace(updateVet.Specialization))
                vetModel.Specialization= updateVet.Specialization;
            
            if(updateVet.BirthDate.HasValue)
                vetModel.BirthDate= updateVet.BirthDate.Value;
            
            if(updateVet.HireDate.HasValue)
                vetModel.HireDate= updateVet.HireDate.Value;    
            
            if(!string.IsNullOrWhiteSpace(updateVet.Activity))
                vetModel.Activity= updateVet.Activity;

            await _context.SaveChangesAsync();
            return Ok(vetModel.ToReadVetDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var vetModel= await _context.Vets.FirstOrDefaultAsync(x=>x.Id==id);
            if (vetModel == null)
            {
                return NotFound();
            }

            _context.Vets.Remove(vetModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}