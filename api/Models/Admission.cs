using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Admission
    {
        public int Id { get; set; }

        // llave foranea hacia Patient
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;  

        // llave foranea hacia Vet
        public int VetId { get; set; }
        public Vet Vet { get; set; } = null!;

        [Required (ErrorMessage ="La fecha de ingreso es obligatorio")]
        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        [Required (ErrorMessage ="El motivo de ingreso es obligatorio")]
        public string AdmissionReason { get; set; } = string.Empty;

        [Required (ErrorMessage ="El numero de jaula es obligatorio")]
        [RegularExpression (@"^\d+$", ErrorMessage ="El numero de jaula solo puede contener numeros")]
        [MaxLength(20, ErrorMessage ="El numero de jaula no puede superar los 20 caracteres")]
        public string CageNumber { get; set; } = string.Empty;

        //relacion uno a muchos con Status
        public ICollection<Status> Statuses { get; set; } = new List<Status>();
    }
}