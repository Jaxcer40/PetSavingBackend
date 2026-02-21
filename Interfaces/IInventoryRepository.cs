using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Inventory;
using PetSavingBackend.Models;
using PetSavingBackend.Helper;

namespace PetSavingBackend.Interfaces
{
    public interface IInventoryRepository
    {
        Task<List<Inventory>> GetAllAsync();
        Task<PagedResponse<Inventory>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Inventory?> GetByIdAsync(int id);
        Task<Inventory> CreateAsync(Inventory inventoryModel);
        Task<Inventory?> PatchAsync(int id, UpdateInventoryDTO updateDTO);
        Task<Inventory?> DeleteAsync(int id);
    }
}