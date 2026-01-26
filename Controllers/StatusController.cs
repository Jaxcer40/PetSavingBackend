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

namespace PetSavingBackend.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StatusController(ApplicationDBContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses=await _context.Statuses
            .Select(s=>s.ToReadStatusDTO()).ToListAsync();

            return Ok(statuses);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var status = await _context.Statuses.Include(a=>a.Admission)
            .FirstOrDefaultAsync(a=>a.Id==id);
            
            if(status== null)
            {
                return NotFound();
            }

            return Ok(status.ToReadStatusDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDTO statusDTO)
        {
            // Validar que el DTO no sea nulo
            if (statusDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PatientId exista
            var admissionExists = await _context.Admissions.AnyAsync(p => p.Id == statusDTO.AdmissionId);
            if (!admissionExists)
                return BadRequest("El AdmissionId no existe.");

            var statusModel = statusDTO.ToStatusFromCreateDTO();
            await _context.Statuses.AddAsync(statusModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id=statusModel.Id}, statusModel.ToReadStatusDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id,  [FromBody] UpdateStatusDTO updateDTO)
        {
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var statusModel= await _context.Statuses.FirstOrDefaultAsync(x=>x.Id == id);

            if (statusModel == null)
            {
                return NotFound();
            }

            if(updateDTO.AdmissionId.HasValue)
            {
                var admissionExists = await _context.Admissions.AnyAsync(p => p.Id == updateDTO.AdmissionId.Value);
                if (!admissionExists)
                    return BadRequest("El AdmissionId no existe.");
                statusModel.AdmissionId= updateDTO.AdmissionId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDTO.CurrentStatus))
                statusModel.CurrentStatus=updateDTO.CurrentStatus;

            if(!string.IsNullOrWhiteSpace(updateDTO.Notes))
                statusModel.Notes=updateDTO.Notes;
            
            await _context.SaveChangesAsync();

            return Ok(statusModel.ToReadStatusDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var statusModel= await _context.Statuses.FirstOrDefaultAsync(x=>x.Id==id);
            if (statusModel == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(statusModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
