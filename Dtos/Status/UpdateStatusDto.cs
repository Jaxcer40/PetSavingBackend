using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Status
{
    public class UpdateStatusDTO
    {
        // llave foranea hacia Admission
        public Guid? AdmissionId { get; set; }

        [MaxLength(100, ErrorMessage ="El estado actual no puede superar los 100 caracteres")]
        public string? CurrentStatus { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
    }
}