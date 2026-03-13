using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Admission;
using System.Data;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Controllers
{
    [Route("api/admission")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionRepository _admissionRepo;
        public AdmissionController(IAdmissionRepository admissionRepo)
        {
            _admissionRepo = admissionRepo;
        }

        //Get de Admission
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admissions = await _admissionRepo.GetAllAsync();
            return Ok(admissions.Select(c => c.ToReadAdmissionDTO()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPetsPaged(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _admissionRepo.GetPagedAsync(pageNumber, pageSize);

            var dtoResponse = new PagedResponse<ReadAdmissionDTO>(
                response.Data.Select(p => p.ToReadAdmissionDTO()).ToList(),
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );

            return Ok(dtoResponse);
        }        

        [HttpGet ("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var admission = await _admissionRepo.GetByIdAsync(id);
            if(admission== null)
            {
                return NotFound();
            }

            return Ok(admission.ToGetOneAdmissionDTOFromAdmission());
        }

        //Post de Admission
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdmissionDTO admissionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el DTO no sea nulo
            if (admissionDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var admissionModel = admissionDTO.ToAdmissionFromCreateDTO();
            var admissionWithPetAndVet = await _admissionRepo.CreateAsync(admissionModel);

            return CreatedAtAction(nameof(GetById), new { id = admissionWithPetAndVet.Id }, admissionWithPetAndVet.ToReadAdmissionDTO());
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateAdmissionDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var admissionModel = await _admissionRepo.PatchAsync(id, updateDTO);
            
            if(admissionModel == null)
            {
                return NotFound();
            }

            return Ok(admissionModel.ToGetOneAdmissionDTOFromAdmission());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var admissionModel= await _admissionRepo.DeleteAsync(id);
            if (admissionModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}