using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Admission
{
    public class UpdateAdmissionDTO
    {
        // llave foranea hacia Pet     
        public Guid? PetId { get; set; }

        // llave foranea hacia Vet
        public Guid? VetId { get; set; }

        public DateTime? AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string? AdmissionReason { get; set; } = string.Empty;

        [RegularExpression (@"^\d+$", ErrorMessage ="El numero de jaula solo puede contener numeros")]
        [MaxLength(20, ErrorMessage ="El numero de jaula no puede superar los 20 caracteres")]
        public string? CageNumber { get; set; } = string.Empty;
    }
}