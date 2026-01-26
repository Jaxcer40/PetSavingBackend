using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Admission
{
    public class UpdateAdmissionDTO
    {
        // llave foranea hacia Patient     
        public int? PatientId { get; set; }

        // llave foranea hacia Vet
        public int? VetId { get; set; }

        public DateTime? AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string? AdmissionReason { get; set; } = string.Empty;

        public string? CageNumber { get; set; } = string.Empty;
    }
}