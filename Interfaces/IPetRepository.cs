using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Models;
using PetSavingBackend.DTOs;
using PetSavingBackend.DTOs.Pet;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Interfaces
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllAsync();
        Task<PagedResponse<Pet>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Pet?> GetByIdAsync(Guid id);
        Task<Pet> CreateAsync(Pet petModel);
        Task<Pet?> PatchAsync(Guid id, UpdatePetDTO updateDTO);
        Task<Pet?> DeleteAsync(Guid id);
    }
}