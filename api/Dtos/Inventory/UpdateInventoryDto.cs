using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Inventory
{
    public class UpdateInventoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? UnitValue { get; set; }
        public int? Stock { get; set; }
        public string? SupplerName { get; set; }
    }
}