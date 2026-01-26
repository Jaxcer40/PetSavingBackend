using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Status
{
    public class UpdateStatusDTO
    {
        // llave foranea hacia Admission
        public int? AdmissionId { get; set; }
        public string? CurrentStatus { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
    }
}