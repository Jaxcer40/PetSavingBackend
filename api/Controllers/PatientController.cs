using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Patient;

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
        public IActionResult GetAll()
        {
            var patients= _context.Patients.ToList()
            .Select(s=>s.ToReadPatientDto());
           

            return Ok(patients);
        
        }

        //Get por Id
        [HttpGet ("{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            var patient = _context.Patients.Find(Id);
            
            if(patient== null)
            {
                return NotFound();
            }

            return Ok(patient.ToReadPatientDto());
        }

        //Post para patient
        [HttpPost]
        public IActionResult Create([FromBody] CreatePatientDto patientDto)
        {
            var patientModel = patientDto.ToPatientFromCreateDto();
            _context.Patients.Add(patientModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {id = patientModel.Id}, patientModel.ToReadPatientDto());
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdatePatientDto updateDto)
        {
            var patientModel= _context.Patients.FirstOrDefault(x=>x.Id==id);

            if (patientModel == null)
            {
                return NotFound();
            }

            if(updateDto.ClientId.HasValue)
            {
                //Aplicar cuando esté Client

                // var clientExists = _context.Clients.Any(p => p.Id == updateDto.ClientId.Value);
                // if (!clientExists)
                // return BadRequest("El ClientId no existe.");
                patientModel.ClientId=updateDto.ClientId.Value;
            }

            if(updateDto.Name!=null)
                patientModel.Name=updateDto.Name;
            
            if (updateDto.Species!=null)
                patientModel.Species=updateDto.Species;
            
            if(updateDto.Breed!=null)
                patientModel.Breed=updateDto.Breed;
            
            if(updateDto.Gender!=null)
                patientModel.Gender=updateDto.Gender;
            
            if(updateDto.BirthDate.HasValue)
                patientModel.BirthDate=updateDto.BirthDate.Value;
            
            if(updateDto.Weight.HasValue)
                patientModel.Weight=updateDto.Weight.Value;

            if(updateDto.AdoptedDate.HasValue)
                patientModel.AdoptedDate=updateDto.AdoptedDate.Value;

            _context.SaveChanges();

            return Ok(patientModel.ToReadPatientDto());

        }
    }    
}