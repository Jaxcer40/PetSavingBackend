using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Status
    {
        public int Id { get; set; }

        // llave foranea hacia Admission
        public int AdmissionId { get; set; }
        public Admission Admission { get; set; } = null!;

        [Required (ErrorMessage ="El estado actual es obligatorio")]
        [MaxLength(100, ErrorMessage ="El estado actual no puede superar los 100 caracteres")]
        public string CurrentStatus { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

    }
}