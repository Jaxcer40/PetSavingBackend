using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Admission
{
    public class CreateAdmissionDto
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