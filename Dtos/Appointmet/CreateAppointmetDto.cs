using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSavingBackend.DTOs.Appointmet
{
    public class CreateAppointmetDTO
    {
        //llave foranea hacia Patient
        [Required]
        public int PatientId {get; set;}

        //llave foranea hacia Client
        [Required]
        public int ClientId {get; set;}
        
        //llave foranea hacia Vet
        [Required]
        public int VetId {get; set;}

        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }
}