using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Patient;
using Microsoft.EntityFrameworkCore;

namespace PetSavingBackend.Controllers
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
            .Select(s=>s.ToReadPatientDTO()).ToListAsync();
           
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

            return Ok(patient.ToReadPatientDTO());
        }

        //Post para patient
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDTO patientDTO)
        {
            // Validar que el DTO no sea nulo
            if (patientDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var patientExists = await _context.Clients.AnyAsync(c => c.Id == patientDTO.ClientId);
            if (!patientExists)
                return BadRequest("El ClientId no existe.");

            var patientModel = patientDTO.ToPatientFromCreateDTO();
            await _context.Patients.AddAsync(patientModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id = patientModel.Id}, patientModel.ToReadPatientDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePatientDTO updateDTO)
        {
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var patientModel= await _context.Patients.FirstOrDefaultAsync(x=>x.Id==id);

            if (patientModel == null)
            {
                return NotFound();
            }

            if(updateDTO.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDTO.ClientId.Value);
                if (!clientExists)
                    return BadRequest("El ClientId no existe.");
                patientModel.ClientId=updateDTO.ClientId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDTO.Name))
                patientModel.Name=updateDTO.Name;
            
            if (!string.IsNullOrWhiteSpace(updateDTO.Species!))
                patientModel.Species=updateDTO.Species;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Breed))
                patientModel.Breed=updateDTO.Breed;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Gender))
                patientModel.Gender=updateDTO.Gender;
            
            if (updateDTO.BirthDate.HasValue && updateDTO.BirthDate.Value > DateTime.UtcNow)
                return BadRequest("Vienes del futuro??? :O");

            if(updateDTO.BirthDate.HasValue)
                patientModel.BirthDate=updateDTO.BirthDate.Value;
            
            if (updateDTO.Weight.HasValue && updateDTO.Weight.Value < 0)
                return BadRequest("El peso no puede ser negativo.");

            if(updateDTO.Weight.HasValue)
                patientModel.Weight=updateDTO.Weight.Value;

            if(updateDTO.AdoptedDate.HasValue)
                patientModel.AdoptedDate=updateDTO.AdoptedDate.Value;

            await _context.SaveChangesAsync();

            return Ok(patientModel.ToReadPatientDTO());
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