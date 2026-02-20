using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Status;
using PetSavingBackend.Models;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepo;
        public StatusController(IStatusRepository statusRepo)
        {
            _statusRepo=statusRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses=await _statusRepo.GetAllAsync();

            return Ok(statuses.Select(a=>a.ToReadStatusDTO()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetSatusPaged(
            int pageNumber=1,
            int pageSize=10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var response = await _statusRepo.GetPagedAsync(pageNumber, pageSize);

            var dtoResponse = new PagedResponse<ReadStatusDTO>(
                response.Data.Select(a => a.ToReadStatusDTO()).ToList(),
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );

            return Ok(dtoResponse);
        }

        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var status = await _statusRepo.GetByIdAsync(id);            
            if(status== null)
            {
                return NotFound();
            }

            return Ok(status.ToReadStatusDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDTO statusDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el DTO no sea nulo
            if (statusDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var statusModel = statusDTO.ToStatusFromCreateDTO();
            var statusWithAdmission = await _statusRepo.CreateAsync(statusModel);

            return CreatedAtAction(nameof(GetById), new {id=statusWithAdmission.Id}, statusWithAdmission.ToReadStatusDTO());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id,  [FromBody] UpdateStatusDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var statusModel= await _statusRepo.PatchAsync(id, updateDTO);

            if (statusModel == null)
            {
                return NotFound();
            }

            return Ok(statusModel.ToReadStatusDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var statusModel= await _statusRepo.DeleteAsync(id);
            if (statusModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
