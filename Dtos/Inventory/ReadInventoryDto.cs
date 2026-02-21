using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Inventory
{
    public class ReadInventoryDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string Description { set; get; } = string.Empty;

        public decimal UnitValue {set; get; }

        public int Stock {set; get; }

        //Aqui se oculta el proveedor

    }
}