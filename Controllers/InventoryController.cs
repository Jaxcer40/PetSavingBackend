using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Inventory;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace PetSavingBackend.Controllers
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
            .Select(s => s.ToReadInventoryDTO()).ToListAsync();

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

            return Ok(inventory.ToReadInventoryDTO());
        }

        //Post para Inventory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryDTO inventoryDTO)
        {
            // Validar que el DTO no sea nulo
            if (inventoryDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");


            var inventoryModel= inventoryDTO.ToInventoryFromCreateDTO();
            await _context.Inventories.AddAsync(inventoryModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new {id=inventoryModel.Id}, inventoryModel.ToReadInventoryDTO());
        }

        //Update por id
        //Path god > Put zzz
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateInventoryDTO updateDTO)
        {
            
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");
            
            var inventoryModel = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(updateDTO.Name))
                inventoryModel.Name = updateDTO.Name;

            if (!string.IsNullOrWhiteSpace(updateDTO.Description))
                inventoryModel.Description = updateDTO.Description;

            if (updateDTO.UnitValue.HasValue)
                inventoryModel.UnitValue = updateDTO.UnitValue.Value;

            if (updateDTO.Stock.HasValue && updateDTO.Stock.Value < 0)
                return BadRequest("El stock no puede ser negativo.");

            if (updateDTO.Stock.HasValue)
                inventoryModel.Stock = updateDTO.Stock.Value;

            if (!string.IsNullOrWhiteSpace(updateDTO.SupplerName))
                inventoryModel.SupplerName = updateDTO.SupplerName;

            await _context.SaveChangesAsync();

            return Ok(inventoryModel.ToReadInventoryDTO());
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