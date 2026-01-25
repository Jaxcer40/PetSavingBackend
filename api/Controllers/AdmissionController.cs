using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Admission;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/admission")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AdmissionController(ApplicationDBContext context)
        {
            _context = context;
        }

        //Get de Admission
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admissions = await _context.Admissions
            .Select(s => s.ToReadAdmissionDto()).ToListAsync();

            return Ok(admissions);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var admission = await _context.Admissions
            .Include(a => a.Patient).Include(a => a.Vet)
            .FirstOrDefaultAsync(a => a.Id == id);
            
            if(admission== null)
            {
                return NotFound();
            }

            return Ok(admission.ToReadAdmissionDto());
        }

        //Post de Admission
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdmissionDto admissionDto)
        {

            // Validar que el DTO no sea nulo
            if (admissionDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            // Validar que el PatientId exista
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == admissionDto.PatientId);
            if (!patientExists)
                return BadRequest("El PatientId no existe.");

            // Validar que el VetId exista (si lo envías en el DTO)
            var vetExists = await _context.Vets.AnyAsync(v => v.Id == admissionDto.VetId);
            if (!vetExists)
                return BadRequest("El VetId no existe.");


            var admissionModel = admissionDto.ToAdmissionFromCreateDto();
            await _context.Admissions.AddAsync(admissionModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id= admissionModel.Id}, admissionModel.ToReadAdmissionDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateAdmissionDto updateDto)
        {
            if (updateDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var admissionModel = await _context.Admissions.FirstOrDefaultAsync(x=>x.Id==id);
            
            if(admissionModel == null)
            {
                return NotFound();
            }

            if(updateDto.PatientId.HasValue)
            {
                var patientExists = await _context.Patients.AnyAsync(p => p.Id == updateDto.PatientId.Value);
                if (!patientExists)
                    return BadRequest("El PatientId no existe.");

                admissionModel.PatientId=updateDto.PatientId.Value;
            }

            if(updateDto.VetId.HasValue)
            {
                var vetExists = await _context.Vets.AnyAsync(v => v.Id == updateDto.VetId.Value);
                if (!vetExists)
                    return BadRequest("El VetId no existe.");

                admissionModel.VetId= updateDto.VetId.Value;
            }

            if(updateDto.AdmissionDate.HasValue)
                admissionModel.AdmissionDate=updateDto.AdmissionDate.Value;
            
            if(updateDto.DischargeDate.HasValue)
                admissionModel.DischargeDate=updateDto.DischargeDate.Value;
            
            if(!string.IsNullOrWhiteSpace(updateDto.AdmissionReason))
                admissionModel.AdmissionReason=updateDto.AdmissionReason;
            
            if(!string.IsNullOrWhiteSpace(updateDto.CageNumber))
                admissionModel.CageNumber=updateDto.CageNumber;

            await _context.SaveChangesAsync();
            
            return Ok(admissionModel.ToReadAdmissionDto());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var admissionModel= await _context.Admissions.FirstOrDefaultAsync(x=>x.Id==id);
            if (admissionModel == null)
            {
                return NotFound();
            }

            _context.Admissions.Remove(admissionModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}