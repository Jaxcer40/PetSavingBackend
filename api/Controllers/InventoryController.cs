using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using api.Data;
using api.Mappers;
using api.Dtos.Inventory;
using Humanizer;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var inventories= await _context.Inventories
            .Select(s => s.ToReadInventoryDto()).ToListAsync();

            return Ok(inventories);
        
        }

        // Get por Id
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            
            if(inventory== null)
            {
                return NotFound();
            }

            return Ok(inventory.ToReadInventoryDto());
        }

        //Post para Inventory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryDto inventoryDto)
        {
            // Validar que el DTO no sea nulo
            if (inventoryDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");


            var inventoryModel= inventoryDto.ToInventoryFromCreateDto();
            await _context.Inventories.AddAsync(inventoryModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id=inventoryModel.Id}, inventoryModel.ToReadInventoryDto());
        }

        //Update por id
        //Path god > Put zzz
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateInventoryDto updateDto)
        {
            
            if (updateDto == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");
            
            var inventoryModel = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                inventoryModel.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
                inventoryModel.Description = updateDto.Description;

            if (updateDto.UnitValue.HasValue)
                inventoryModel.UnitValue = updateDto.UnitValue.Value;

            if (updateDto.Stock.HasValue && updateDto.Stock.Value < 0)
                return BadRequest("El stock no puede ser negativo.");

            if (updateDto.Stock.HasValue)
                inventoryModel.Stock = updateDto.Stock.Value;

            if (!string.IsNullOrWhiteSpace(updateDto.SupplerName))
                inventoryModel.SupplerName = updateDto.SupplerName;

            await _context.SaveChangesAsync();

            return Ok(inventoryModel.ToReadInventoryDto());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var inventoryModel= await _context.Inventories.FirstOrDefaultAsync(x=>x.Id==id);
            if (inventoryModel == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventoryModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}