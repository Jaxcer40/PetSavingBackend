using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Status;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
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
            .Select(s=>s.ToReadStatusDto()).ToListAsync();

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

            return Ok(status.ToReadStatusDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusDto statusDto)
        {
            // Validar que el DTO no sea nulo
            if (statusDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PatientId exista
            var admissionExists = await _context.Admissions.AnyAsync(p => p.Id == statusDto.AdmissionId);
            if (!admissionExists)
                return BadRequest("El AdmissionId no existe.");

            var statusModel = statusDto.ToStatusFromCreateDto();
            await _context.Statuses.AddAsync(statusModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id=statusModel.Id}, statusModel.ToReadStatusDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id,  [FromBody] UpdateStatusDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var statusModel= await _context.Statuses.FirstOrDefaultAsync(x=>x.Id == id);

            if (statusModel == null)
            {
                return NotFound();
            }

            if(updateDto.AdmissionId.HasValue)
            {
                var admissionExists = await _context.Admissions.AnyAsync(p => p.Id == updateDto.AdmissionId.Value);
                if (!admissionExists)
                    return BadRequest("El AdmissionId no existe.");
                statusModel.AdmissionId= updateDto.AdmissionId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDto.CurrentStatus))
                statusModel.CurrentStatus=updateDto.CurrentStatus;

            if(!string.IsNullOrWhiteSpace(updateDto.Notes))
                statusModel.Notes=updateDto.Notes;
            
            await _context.SaveChangesAsync();

            return Ok(statusModel.ToReadStatusDto());
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
