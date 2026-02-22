using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Appointment;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepo;
        public AppointmentController(IAppointmentRepository appointmentRepo)
        {
            _appointmentRepo=appointmentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments= await _appointmentRepo.GetAllAsync();
            return Ok(appointments.Select(c=>c.ToReadAppointmentDTO()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPetsPaged(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _appointmentRepo.GetPagedAsync(pageNumber, pageSize);

            var dtoResponse = new PagedResponse<ReadAppointmentDTO>(
                response.Data.Select(p => p.ToReadAppointmentDTO()).ToList(),
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );

            return Ok(dtoResponse);
        }

        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if(appointment== null)
            {
                return NotFound();
            }

            return Ok(appointment.ToReadAppointmentDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO appointmentDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Validar que el DTO no sea nulo
            if (appointmentDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var appointmentModel= appointmentDTO.ToAppointmentFromCreateDTO();
            var appointmentWithPetAndClientAndVet= await _appointmentRepo.CreateAsync(appointmentModel);

            return CreatedAtAction(nameof(GetById), new {id= appointmentWithPetAndClientAndVet.Id}, appointmentWithPetAndClientAndVet.ToReadAppointmentDTO());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateAppointmentDTO updateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(updateDTO ==null)
                return BadRequest("El cuerpo de la solicitud esta vacio");

            var appointmentModel=await _appointmentRepo.PatchAsync(id, updateDTO);

            if (appointmentModel == null)
            {
                return NotFound();
            }

            return Ok(appointmentModel.ToReadAppointmentDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var appointmetModel= await _appointmentRepo.DeleteAsync(id);
            if (appointmetModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}