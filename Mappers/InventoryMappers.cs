using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Inventory;
using PetSavingBackend.Models;
using Humanizer;

namespace PetSavingBackend.Mappers
{
    public static class InventoryMappers
    {
        public static ReadInentoryDTO ToReadInventoryDTO(this Inventory inventoryModel)
        {
            return new ReadInentoryDTO
            {
                Id= inventoryModel.Id,
                Name= inventoryModel.Name,
                Description=inventoryModel.Description,
                UnitValue= inventoryModel.UnitValue,
                Stock= inventoryModel.Stock,
            };
        }

        public static Inventory ToInventoryFromCreateDTO(this CreateInventoryDTO inventoryDTO)
        {
            return new Inventory
            {
                Name= inventoryDTO.Name,
                Description= inventoryDTO.Description,
                UnitValue=inventoryDTO.UnitValue,
                Stock=inventoryDTO.Stock,
                SupplerName=inventoryDTO.SupplerName

            };
        }
    }
}