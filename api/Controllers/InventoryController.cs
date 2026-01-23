using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Inventory;
using Humanizer;

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

        // Get por Id
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

        //Post para Inventory
        [HttpPost]
        public IActionResult Create([FromBody] CreateInventoryDto inventoryDto)
        {
            var inventoryModel= inventoryDto.ToInventoryFromCreateDto();
            _context.Inventories.Add(inventoryModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),new {id=inventoryModel.Id}, inventoryModel.ToReadInventoryDto());
        }

        //Update por id
        //Path god > Put zzz
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateInventoryDto updateDto)
        {
            var inventoryModel = _context.Inventories.FirstOrDefault(x => x.Id == id);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            if (updateDto.Name != null)
                inventoryModel.Name = updateDto.Name;

            if (updateDto.Description != null)
                inventoryModel.Description = updateDto.Description;

            if (updateDto.UnitValue.HasValue)
                inventoryModel.UnitValue = updateDto.UnitValue.Value;

            if (updateDto.Stock.HasValue)
                inventoryModel.Stock = updateDto.Stock.Value;

            if (updateDto.SupplerName != null)
                inventoryModel.SupplerName = updateDto.SupplerName;

            _context.SaveChanges();

            return Ok(inventoryModel.ToReadInventoryDto());
        }
    }
}