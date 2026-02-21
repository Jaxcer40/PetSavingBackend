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
using PetSavingBackend.Interfaces;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepo;
        public InventoryController(IInventoryRepository inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories= await _inventoryRepo.GetAllAsync();
            return Ok(inventories.Select(i=>i.ToReadInventoryDTO()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPetsPaged(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _inventoryRepo.GetPagedAsync(pageNumber, pageSize);

            var dtoResponse = new PagedResponse<ReadInventoryDTO>(
                response.Data.Select(p => p.ToReadInventoryDTO()).ToList(),
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );

            return Ok(dtoResponse);
        }

        // Get por Id
        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var inventory = await _inventoryRepo.GetByIdAsync(id);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Validar que el DTO no sea nulo
            if (inventoryDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var inventoryModel=inventoryDTO.ToInventoryFromCreateDTO();
            var invrntoryWithDetails = await _inventoryRepo.CreateAsync(inventoryModel);
            
            return CreatedAtAction(nameof(GetById),new {id=invrntoryWithDetails.Id}, invrntoryWithDetails.ToReadInventoryDTO());
        }

        //Update por id
        //Path god > Put zzz
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateInventoryDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");
            
            var inventoryModel = await _inventoryRepo.PatchAsync(id, updateDTO);

            if (inventoryModel == null)
            {
                return NotFound();
            }

            return Ok(inventoryModel.ToReadInventoryDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var inventoryModel= await _inventoryRepo.DeleteAsync(id);
            if (inventoryModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}