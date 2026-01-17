using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Inventory;
using api.Models;

namespace api.Mappers
{
    public static class InventoryMappers
    {
        public static ReadInentoryDto ToReadInventoryDto(this Inventory inventoryModel)
        {
            return new ReadInentoryDto
            {
                Id= inventoryModel.Id,
                Name= inventoryModel.Name,
                Description=inventoryModel.Description,
                UnitValue= inventoryModel.UnitValue,
                Stock= inventoryModel.Stock
            };
        }
    }
}