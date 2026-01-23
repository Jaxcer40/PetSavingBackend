using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Admission;
using System.Data;

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
        public IActionResult GetAll()
        {
            var admissions= _context.Admissions.ToList()
            .Select(s=>s.ToReadAdmissionDto());
           

            return Ok(admissions);
        
        }

        [HttpGet ("{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            var admission = _context.Admissions.Find(Id);
            
            if(admission== null)
            {
                return NotFound();
            }

            return Ok(admission.ToReadAdmissionDto());
        }

        //Post de Admission
        [HttpPost]
        public IActionResult Create([FromBody] CreateAdmissionDto admissionDto)
        {
            var admissionModel = admissionDto.ToAdmissionFromCreateDto();
            _context.Admissions.Add(admissionModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id= admissionModel.Id}, admissionModel.ToReadAdmissionDto());
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateAdmissionDto updateDto)
        {
            var admissionModel = _context.Admissions.FirstOrDefault(x=>x.Id==id);
            
            if(admissionModel == null)
            {
                return NotFound();
            }

            if(updateDto.PatientId.HasValue)
            {
                var patientExists = _context.Patients.Any(p => p.Id == updateDto.PatientId.Value);
                if (!patientExists)
                return BadRequest("El PatientId no existe.");

                admissionModel.PatientId=updateDto.PatientId.Value;
            }

            if(updateDto.VetId.HasValue)
            {
                //Aplicar cuando esté Vet

                // var vetExists = _context.Vets.Any(p => p.Id == updateDto.VetId.Value);
                // if (!vetExists)
                // return BadRequest("El VetId no existe.");


                admissionModel.VetId= updateDto.VetId.Value;
            }

            if(updateDto.AdmissionDate.HasValue)
                admissionModel.AdmissionDate=updateDto.AdmissionDate.Value;
            
            if(updateDto.DischargeDate.HasValue)
                admissionModel.DischargeDate=updateDto.DischargeDate.Value;
            
            if(updateDto.AdmissionReason!=null)
                admissionModel.AdmissionReason=updateDto.AdmissionReason;
            
            if(updateDto.CageNumber!=null)
                admissionModel.CageNumber=updateDto.CageNumber;

            _context.SaveChanges();
            
            return Ok(admissionModel.ToReadAdmissionDto());
        }

    }
}