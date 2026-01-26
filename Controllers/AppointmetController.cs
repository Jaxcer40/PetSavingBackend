using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Appointmet;
using Microsoft.EntityFrameworkCore;

namespace PetSavingBackend.Controllers
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
            .Select(s=> s.ToReadAppointmetDTO()).ToListAsync();
           
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

            return Ok(appointment.ToReadAppointmetDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmetDTO appointmetDTO)
        {
            // Validar que el DTO no sea nulo
            if (appointmetDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PatientId exista
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == appointmetDTO.PatientId);
            if (!patientExists)
                return BadRequest("El PatientId no existe.");

            // Validar que el ClientId exista
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == appointmetDTO.ClientId);
            if (!clientExists)
                return BadRequest("El PatientId no existe.");

            // Validar que el VetId exista (si lo envías en el DTO)
            var vetExists = await _context.Vets.AnyAsync(v => v.Id == appointmetDTO.VetId);
            if (!vetExists)
                return BadRequest("El VetId no existe.");

            var appointmetModel= appointmetDTO.ToAppointmetFromCreateDTO();
            await _context.Appointmets.AddAsync(appointmetModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id= appointmetModel.Id}, appointmetModel.ToReadAppointmetDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateAppointmetDTO updateDTO)
        {
            var appointmetModel= await _context.Appointmets.FirstOrDefaultAsync(x=>x.Id==id);

            if (appointmetModel == null)
            {
                return NotFound();
            }

            if(updateDTO.PatientId.HasValue)
            {
                var patientExists = await _context.Patients.AnyAsync(p => p.Id == updateDTO.PatientId.Value);
                if (!patientExists)
                return BadRequest("El PatientId no existe.");
                appointmetModel.PatientId=updateDTO.PatientId.Value;
            }

            if(updateDTO.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDTO.ClientId.Value);
                if (!clientExists)
                    return BadRequest("El ClientId no existe.");

                appointmetModel.ClientId=updateDTO.ClientId.Value;
            }

            if(updateDTO.VetId.HasValue)
            {
                var vetExists = await _context.Vets.AnyAsync(v => v.Id == updateDTO.VetId.Value);
                if (!vetExists)
                    return BadRequest("El VetId no existe.");
                appointmetModel.VetId=updateDTO.VetId.Value;
            }

            if(updateDTO.AppointmentDate.HasValue)
                appointmetModel.AppointmentDate=updateDTO.AppointmentDate.Value;
            
            if(updateDTO.Diagnosis!=null)
                appointmetModel.Diagnosis=updateDTO.Diagnosis;
            
            if(updateDTO.Treatment!=null)
                appointmetModel.Treatment=updateDTO.Treatment;
            
            if(updateDTO.Notes!=null)
                appointmetModel.Notes=updateDTO.Notes;
            
            if(updateDTO.FollowUpDate.HasValue)
                appointmetModel.FollowUpDate=updateDTO.FollowUpDate.Value;

            await _context.SaveChangesAsync();

            return Ok(appointmetModel.ToReadAppointmetDTO());

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