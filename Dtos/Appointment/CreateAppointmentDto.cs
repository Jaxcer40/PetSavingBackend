using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSavingBackend.DTOs.Appointment
{
    public class CreateAppointmentDTO
    {
        //llave foranea hacia Pet
        [Required]
        public int PetId {get; set;}

        //llave foranea hacia Client
        [Required]
        public int ClientId {get; set;}
        
        //llave foranea hacia Vet
        [Required]
        public int VetId {get; set;}

        public DateTime AppointmentDate { get; set; }

        [Required (ErrorMessage ="El diagnostico es obligatorio")]
        public string Diagnosis { get; set; } = string.Empty;

        [Required (ErrorMessage ="El tratamiento es obligatorio")]
        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }
}