using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Appointmet;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/appointmet")]
    [ApiController]
    public class AppointmetController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AppointmetController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointmets= await _context.Appointmets
            .Select(s=> s.ToReadAppointmetDto()).ToListAsync();
           
            return Ok(appointmets);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var appointment = await _context.Appointmets
            .Include(a=>a.Patient).Include(a=>a.Client)
            .Include(a=>a.Vet).FirstOrDefaultAsync(a=>a.Id==id);
            
            if(appointment== null)
            {
                return NotFound();
            }

            return Ok(appointment.ToReadAppointmetDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmetDto appointmetDto)
        {
            // Validar que el DTO no sea nulo
            if (appointmetDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PatientId exista
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == appointmetDto.PatientId);
            if (!patientExists)
                return BadRequest("El PatientId no existe.");

            // Validar que el ClientId exista
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == appointmetDto.ClientId);
            if (!clientExists)
                return BadRequest("El PatientId no existe.");

            // Validar que el VetId exista (si lo envías en el DTO)
            var vetExists = await _context.Vets.AnyAsync(v => v.Id == appointmetDto.VetId);
            if (!vetExists)
                return BadRequest("El VetId no existe.");

            var appointmetModel= appointmetDto.ToAppointmetFromCreateDto();
            await _context.Appointmets.AddAsync(appointmetModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id= appointmetModel.Id}, appointmetModel.ToReadAppointmetDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateAppointmetDto updateDto)
        {
            var appointmetModel= await _context.Appointmets.FirstOrDefaultAsync(x=>x.Id==id);

            if (appointmetModel == null)
            {
                return NotFound();
            }

            if(updateDto.PatientId.HasValue)
            {
                var patientExists = await _context.Patients.AnyAsync(p => p.Id == updateDto.PatientId.Value);
                if (!patientExists)
                return BadRequest("El PatientId no existe.");
                appointmetModel.PatientId=updateDto.PatientId.Value;
            }

            if(updateDto.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDto.ClientId.Value);
                if (!clientExists)
                    return BadRequest("El ClientId no existe.");

                appointmetModel.ClientId=updateDto.ClientId.Value;
            }

            if(updateDto.VetId.HasValue)
            {
                var vetExists = await _context.Vets.AnyAsync(v => v.Id == updateDto.VetId.Value);
                if (!vetExists)
                    return BadRequest("El VetId no existe.");
                appointmetModel.VetId=updateDto.VetId.Value;
            }

            if(updateDto.AppointmentDate.HasValue)
                appointmetModel.AppointmentDate=updateDto.AppointmentDate.Value;
            
            if(updateDto.Diagnosis!=null)
                appointmetModel.Diagnosis=updateDto.Diagnosis;
            
            if(updateDto.Treatment!=null)
                appointmetModel.Treatment=updateDto.Treatment;
            
            if(updateDto.Notes!=null)
                appointmetModel.Notes=updateDto.Notes;
            
            if(updateDto.FollowUpDate.HasValue)
                appointmetModel.FollowUpDate=updateDto.FollowUpDate.Value;

            await _context.SaveChangesAsync();

            return Ok(appointmetModel.ToReadAppointmetDto());

        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var appointmetModel= await _context.Appointmets.FirstOrDefaultAsync(x=>x.Id==id);
            if (appointmetModel == null)
            {
                return NotFound();
            }

            _context.Appointmets.Remove(appointmetModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}