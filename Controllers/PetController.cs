using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PetSavingBackend.Data;
using PetSavingBackend.Mappers;
using PetSavingBackend.DTOs.Pet;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Controllers
{
    [Route("api/pet")]
    [ApiController]

    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepo;
        public PetController(IPetRepository petRepo)
        {
            _petRepo=petRepo;
        }

        //Get de Pet
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _petRepo.GetAllAsync();
            return Ok(pets.Select(c=>c.ToReadPetDTO()));
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPetsPaged(
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _petRepo.GetPagedAsync(pageNumber, pageSize);

            var dtoResponse = new PagedResponse<ReadPetDTO>(
                response.Data.Select(p => p.ToReadPetDTO()).ToList(),
                response.TotalRecords,
                response.PageNumber,
                response.PageSize
            );

            return Ok(dtoResponse);
        }

        //Get por Id
        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var pet = await _petRepo.GetByIdAsync(id);
            if(pet== null)
            {
                return NotFound();
            }

            return Ok(pet.ToReadPetDTO());
        }

        //Post para pet
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDTO petDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Validar que el DTO no sea nulo
            if (petDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var petModel = petDTO.ToPetFromCreateDTO();
            var petWithClient = await _petRepo.CreateAsync(petModel);

            return CreatedAtAction(nameof(GetById),new {id = petWithClient.Id}, petWithClient.ToReadPetDTO());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePetDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (updateDTO == null)
                return BadRequest("El cuerpo de la solicitud está vacío.");

            var petModel= await _petRepo.PatchAsync(id, updateDTO);

            if (petModel == null)
            {
                return NotFound();
            }

            return Ok(petModel.ToReadPetDTO());
        }

        //Delete por id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var petModel=  await _petRepo.DeleteAsync(id);
            if (petModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }    
}