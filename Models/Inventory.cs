using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { set; get; } = string.Empty;

        public decimal UnitValue {set; get; }

        public int Stock {set; get; }

        public string SupplerName {set; get; }= string.Empty;

    }
}