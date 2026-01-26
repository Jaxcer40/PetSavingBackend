using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Inventory
{
    public class CreateInventoryDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { set; get; } = string.Empty;

        [Required]
        public decimal UnitValue {set; get; }
        [Required]
        public int Stock {set; get; }
        
        public string SupplerName {set;get;} = string.Empty;
    }
}