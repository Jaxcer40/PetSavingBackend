using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Data;
using PetSavingBackend.DTOs.Inventory;
using PetSavingBackend.Helper;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;

namespace PetSavingBackend.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDBContext _context;
        public InventoryRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Inventory> CreateAsync(Inventory inventoryModel)
        {
            await _context.Inventories.AddAsync(inventoryModel);
            await _context.SaveChangesAsync();

            var inventoryWithDetails = await _context.Inventories
                .FirstOrDefaultAsync(i => i.Id == inventoryModel.Id);
            
            return inventoryWithDetails!;
        }

        public async Task<Inventory?> DeleteAsync(int id)
        {
            var inventoryModel = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryModel == null)
            {
                return null;
            }

            _context.Inventories.Remove(inventoryModel);
            await _context.SaveChangesAsync();
            return inventoryModel;
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<PagedResponse<Inventory>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Inventories.OrderBy(i=>i.Id).AsQueryable();

            var totalRecords = await query.CountAsync();

            var inventories = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Inventory>(inventories, totalRecords, pageNumber, pageSize);
        }

        public async Task<Inventory?> PatchAsync(int id, UpdateInventoryDTO updateDTO)
        {
            var existingInventory = await _context.Inventories.FindAsync(id);
            if (existingInventory == null) return null;

            if (!string.IsNullOrEmpty(updateDTO.Name))
                existingInventory.Name = updateDTO.Name;
            
            if (!string.IsNullOrEmpty(updateDTO.Description))
                existingInventory.Description = updateDTO.Description;
            
            if (updateDTO.UnitValue.HasValue)
                existingInventory.UnitValue = updateDTO.UnitValue.Value;
            
            if (updateDTO.Stock.HasValue && updateDTO.Stock.Value < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            
            if (updateDTO.Stock.HasValue)
                existingInventory.Stock = updateDTO.Stock.Value;   
            
            if (!string.IsNullOrEmpty(updateDTO.SupplerName))
                existingInventory.SupplerName = updateDTO.SupplerName;

            await _context.SaveChangesAsync();
            var inventoryWithDetails = await _context.Inventories
                .FirstOrDefaultAsync(i => i.Id == existingInventory.Id);
            
            return existingInventory;
        }
    }
}