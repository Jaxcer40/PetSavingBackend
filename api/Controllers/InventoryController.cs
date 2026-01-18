using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Inventory;

namespace api.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public InventoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var inventories= _context.Inventories.ToList()
            .Select(s => s.ToReadInventoryDto());

            return Ok(inventories);
        
        }

        [HttpGet ("{Id}")]
        public IActionResult GetById([FromRoute] int Id)
        {
            var inventory = _context.Inventories.Find(Id);
            
            if(inventory== null)
            {
                return NotFound();
            }

            return Ok(inventory.ToReadInventoryDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateInventoryDto inventoryDto)
        {
            var inventoryModel= inventoryDto.ToInventoryFromCreateDto();
            _context.Inventories.Add(inventoryModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {id=inventoryModel.Id}, inventoryModel.ToReadInventoryDto());
        }

    }
}