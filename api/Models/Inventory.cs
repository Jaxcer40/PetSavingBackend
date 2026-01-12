using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "El nombre es obligatorio")]
        [MaxLength (100, ErrorMessage ="El nombre no puede superar los 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        public string Description { set; get; } = string.Empty;

        [Required (ErrorMessage ="El precio unitario es obligatorio")]
        [Column(TypeName= "decimal(10,2)")]
        public decimal UnitValue {set; get; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock {set; get; }

        [Required (ErrorMessage ="El nombre del proveedor es obligatorio")]
        [MaxLength(100, ErrorMessage ="EL nombre del proveedor no puede superar los 100 caracteres")]
        public string SupplerName {set; get; }= string.Empty;

    }
}