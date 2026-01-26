using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Patient;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;

namespace PetSavingBackend.Controllers
{
    [Route("api/patient")]
    [ApiController]

    //Get de Patient
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;
        public PatientController(IPatientRepository patientRepo)
        {
            _patientRepo=patientRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientRepo.GetAllAsync();
            return Ok(patients.Select(c=>c.ToReadPatientDTO()));
        }


        //Get por Id
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
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

            var patientModel = patientDTO.ToPatientFromCreateDTO();
            await _patientRepo.CreateAsync(patientModel);
            return CreatedAtAction(nameof(GetById),new {id = patientModel.Id}, patientModel.ToReadPatientDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePatientDTO updateDTO)
        {
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var patientModel= await _patientRepo.PatchAsync(id, updateDTO);

            if (patientModel == null)
            {
                return NotFound();
            }

            return Ok(patientModel.ToReadPatientDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var patientModel=  await _patientRepo.DeleteAsync(id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }    
}