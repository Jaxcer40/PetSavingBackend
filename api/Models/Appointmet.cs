using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Appointmet
    {
        public int Id { get; set; }

        // llave foranea hacia Patient
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        // llave foranea hacia Client
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        // llave foranea hacia Vet
        public int VetId { get; set; }
        public Vet Vet { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        [Required (ErrorMessage ="El diagnostico es obligatorio")]
        public string Diagnosis { get; set; } = string.Empty;

        [Required (ErrorMessage ="El tratamiento es obligatorio")]
        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }
}