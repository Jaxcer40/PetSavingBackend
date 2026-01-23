using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Appointmet;

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
        public IActionResult GetAll()
        {
            var appointmets= _context.Appointmets.ToList()
            .Select( s=> s.ToReadAppointmetDto());
           

            return Ok(appointmets);
        
        }

        [HttpGet ("{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            var appointment = _context.Appointmets.Find(Id);
            
            if(appointment== null)
            {
                return NotFound();
            }

            return Ok(appointment.ToReadAppointmetDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAppointmetDto appointmetDto)
        {
            var appointmetModel= appointmetDto.ToAppointmetFromCreateDto();
            _context.Appointmets.Add(appointmetModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id= appointmetModel.Id}, appointmetModel.ToReadAppointmetDto());
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateAppointmetDto updateDto)
        {
            var appointmetModel= _context.Appointmets.FirstOrDefault(x=>x.Id==id);

            if (appointmetModel == null)
            {
                return NotFound();
            }

            if(updateDto.PatientId.HasValue)
            {
                var patientExists = _context.Patients.Any(p => p.Id == updateDto.PatientId.Value);
                if (!patientExists)
                return BadRequest("El PatientId no existe.");
                appointmetModel.PatientId=updateDto.PatientId.Value;
            }

            if(updateDto.ClientId.HasValue)
            {
                //Aplicar cuando esté Client

                // var clientExists = _context.Clients.Any(p => p.Id == updateDto.ClientId.Value);
                // if (!clientExists)
                // return BadRequest("El ClientId no existe.");
                appointmetModel.ClientId=updateDto.ClientId.Value;
            }

            if(updateDto.VetId.HasValue)
            {
                //Aplicar cuando esté Vet

                // var vetExists = _context.Vets.Any(p => p.Id == updateDto.VetId.Value);
                // if (!vetExists)
                // return BadRequest("El VetId no existe.");
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

            _context.SaveChanges();

            return Ok(appointmetModel.ToReadAppointmetDto());

        }

    }
}