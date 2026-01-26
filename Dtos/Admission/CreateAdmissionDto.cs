using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Admission
{
    public class CreateAdmissionDTO
    {
        // llave foranea hacia Patient
        [Required]
        public int PatientId { get; set; }

        // llave foranea hacia Vet
        [Required]
        public int VetId { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        [Required]
        public string AdmissionReason { get; set; } = string.Empty;

        public string CageNumber { get; set; } = string.Empty;
    }

}