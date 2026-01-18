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

    }
}