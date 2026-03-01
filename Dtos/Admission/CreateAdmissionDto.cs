using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Admission
{
    public class CreateAdmissionDTO
    {
        // llave foranea hacia Pet
        [Required]
        public Guid PetId { get; set; }

        // llave foranea hacia Vet
        [Required]
        public Guid VetId { get; set; }

        [Required (ErrorMessage ="La fecha de ingreso es obligatorio")]
        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        [Required (ErrorMessage ="El motivo de ingreso es obligatorio")]
        public string AdmissionReason { get; set; } = string.Empty;

        [Required (ErrorMessage ="El numero de jaula es obligatorio")]
        [RegularExpression (@"^\d+$", ErrorMessage ="El numero de jaula solo puede contener numeros")]
        [MaxLength(20, ErrorMessage ="El numero de jaula no puede superar los 20 caracteres")]
        public string CageNumber { get; set; } = string.Empty;
    }

}