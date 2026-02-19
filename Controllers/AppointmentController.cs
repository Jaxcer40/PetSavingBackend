using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Appointment;
using Microsoft.EntityFrameworkCore;

namespace PetSavingBackend.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AppointmentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments= await _context.Appointments
            .Select(s=> s.ToReadAppointmentDTO()).ToListAsync();
           
            return Ok(appointments);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var appointment = await _context.Appointments
            .Include(a=>a.Pet).Include(a=>a.Client)
            .Include(a=>a.Vet).FirstOrDefaultAsync(a=>a.Id==id);
            
            if(appointment== null)
            {
                return NotFound();
            }

            return Ok(appointment.ToReadAppointmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO appointmentDTO)
        {
            // Validar que el DTO no sea nulo
            if (appointmentDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PetId exista
            var petExists = await _context.Pets.AnyAsync(p => p.Id == appointmentDTO.PetId);
            if (!petExists)
                return BadRequest("El PetId no existe.");

            // Validar que el ClientId exista
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == appointmentDTO.ClientId);
            if (!clientExists)
                return BadRequest("El ClientId no existe.");

            // Validar que el VetId exista (si lo envías en el DTO)
            var vetExists = await _context.Vets.AnyAsync(v => v.Id == appointmentDTO.VetId);
            if (!vetExists)
                return BadRequest("El VetId no existe.");

            var appointmentModel= appointmentDTO.ToAppointmentFromCreateDTO();
            await _context.Appointments.AddAsync(appointmentModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id= appointmentModel.Id}, appointmentModel.ToReadAppointmentDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateAppointmentDTO updateDTO)
        {
            var appointmetModel= await _context.Appointments.FirstOrDefaultAsync(x=>x.Id==id);

            if (appointmetModel == null)
            {
                return NotFound();
            }

            if(updateDTO.PetId.HasValue)
            {
                var petExists = await _context.Pets.AnyAsync(p => p.Id == updateDTO.PetId.Value);
                if (!petExists)
                return BadRequest("El PetId no existe.");
                appointmetModel.PetId=updateDTO.PetId.Value;
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

            return Ok(appointmetModel.ToReadAppointmentDTO());

        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var appointmetModel= await _context.Appointments.FirstOrDefaultAsync(x=>x.Id==id);
            if (appointmetModel == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointmetModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}