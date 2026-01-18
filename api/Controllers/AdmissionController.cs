using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Admission;

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

    }
}