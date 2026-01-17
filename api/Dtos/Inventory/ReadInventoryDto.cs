using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Inventory
{
    public class ReadInentoryDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string Description { set; get; } = string.Empty;

        public decimal UnitValue {set; get; }

        public int Stock {set; get; }

    }
}