using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Patient;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/patient")]
    [ApiController]

    //Get de Patient
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public PatientController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients=await _context.Patients
            .Select(s=>s.ToReadPatientDto()).ToListAsync();
           
            return Ok(patients);
        
        }

        //Get por Id
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _context.Patients.Include(a=>a.Client)
            .FirstOrDefaultAsync(a=>a.Id==id);
            
            if(patient== null)
            {
                return NotFound();
            }

            return Ok(patient.ToReadPatientDto());
        }

        //Post para patient
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto patientDto)
        {
            // Validar que el DTO no sea nulo
            if (patientDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var patientExists = await _context.Clients.AnyAsync(c => c.Id == patientDto.ClientId);
            if (!patientExists)
                return BadRequest("El ClientId no existe.");

            var patientModel = patientDto.ToPatientFromCreateDto();
            await _context.Patients.AddAsync(patientModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id = patientModel.Id}, patientModel.ToReadPatientDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePatientDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var patientModel= await _context.Patients.FirstOrDefaultAsync(x=>x.Id==id);

            if (patientModel == null)
            {
                return NotFound();
            }

            if(updateDto.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDto.ClientId.Value);
                if (!clientExists)
                    return BadRequest("El ClientId no existe.");
                patientModel.ClientId=updateDto.ClientId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDto.Name))
                patientModel.Name=updateDto.Name;
            
            if (!string.IsNullOrWhiteSpace(updateDto.Species!))
                patientModel.Species=updateDto.Species;
            
            if(!string.IsNullOrWhiteSpace(updateDto.Breed))
                patientModel.Breed=updateDto.Breed;
            
            if(!string.IsNullOrWhiteSpace(updateDto.Gender))
                patientModel.Gender=updateDto.Gender;
            
            if (updateDto.BirthDate.HasValue && updateDto.BirthDate.Value > DateTime.UtcNow)
                return BadRequest("Vienes del futuro??? :O");

            if(updateDto.BirthDate.HasValue)
                patientModel.BirthDate=updateDto.BirthDate.Value;
            
            if (updateDto.Weight.HasValue && updateDto.Weight.Value < 0)
                return BadRequest("El peso no puede ser negativo.");

            if(updateDto.Weight.HasValue)
                patientModel.Weight=updateDto.Weight.Value;

            if(updateDto.AdoptedDate.HasValue)
                patientModel.AdoptedDate=updateDto.AdoptedDate.Value;

            await _context.SaveChangesAsync();

            return Ok(patientModel.ToReadPatientDto());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var patientModel=  await _context.Patients.FirstOrDefaultAsync(x=>x.Id==id);
            if (patientModel == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patientModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }    
}