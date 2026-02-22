using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Data;
using PetSavingBackend.Interfaces;
using PetSavingBackend.DTOs;
using PetSavingBackend.DTOs.Pet;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Mappers;
using PetSavingBackend.Models;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDBContext _context;
        public PetRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Pet> CreateAsync(Pet petModel)
        {
            await _context.Pets.AddAsync(petModel);
            await _context.SaveChangesAsync();

            var petWithClient = await _context.Pets
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.Id == petModel.Id);

            return petWithClient!;
        }

        public async Task<Pet?> DeleteAsync(int id)
        {
            var petModel= await _context.Pets.FirstOrDefaultAsync(x=>x.Id==id);

            if (petModel == null)
            {
                return null;
            }

            _context.Pets.Remove(petModel);
            await _context.SaveChangesAsync();
            return petModel;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await _context.Pets
                .Include(p => p.Client)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<PagedResponse<Pet>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Pets.Include(p => p.Client).OrderBy(p => p.Id).AsQueryable();

            var totalRecords = await query.CountAsync();

            var pets = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Pet>(pets, totalRecords, pageNumber, pageSize);
        }

        public async Task<Pet?> PatchAsync(int id, UpdatePetDTO updateDTO)
        {
            var existingPet=await _context.Pets.FindAsync(id);
            if (existingPet==null) return null;
            
            if(updateDTO.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDTO.ClientId.Value);
                if (!clientExists)
                    throw new ArgumentException("El ClientId no existe.");

                existingPet.ClientId=updateDTO.ClientId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDTO.Name))
                existingPet.Name=updateDTO.Name;
            
            if (!string.IsNullOrWhiteSpace(updateDTO.Species))
                existingPet.Species=updateDTO.Species;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Breed))
                existingPet.Breed=updateDTO.Breed;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Gender))
                existingPet.Gender=updateDTO.Gender;
            
            if (updateDTO.BirthDate.HasValue && updateDTO.BirthDate.Value > DateTime.UtcNow)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura");

            if(updateDTO.BirthDate.HasValue)
                existingPet.BirthDate=updateDTO.BirthDate.Value;
            
            if (updateDTO.Weight.HasValue && updateDTO.Weight.Value < 0)
                throw new ArgumentException("El peso no puede ser menor a 0");

            if(updateDTO.Weight.HasValue)
                existingPet.Weight=updateDTO.Weight.Value;

            if(updateDTO.AdoptedDate.HasValue)
                existingPet.AdoptedDate=updateDTO.AdoptedDate.Value;
            
            if (updateDTO.Rating.HasValue && updateDTO.Rating.Value < 0)
                throw new ArgumentException("El rating no puede ser negativo");

            if(updateDTO.Rating.HasValue)
                existingPet.Rating=updateDTO.Rating.Value;

            await _context.SaveChangesAsync();
            
            var petWithClient = await _context.Pets
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.Id == existingPet.Id);
    
            return petWithClient;
        }
    }
}