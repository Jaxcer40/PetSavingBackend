using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Appointment
{
    public class UpdateAppointmentDTO
    {
        //llave foranea hacia Pet
        public int? PetId {get; set;}

        //llave foranea hacia Client
        public int? ClientId {get; set;}
        
        //llave foranea hacia Vet
        public int? VetId {get; set;}

        public DateTime? AppointmentDate { get; set; }

        public string? Diagnosis { get; set; } = string.Empty;

        public string? Treatment { get; set; } = string.Empty;

        public string? Notes { get; set; } = string.Empty;

        public DateOnly? FollowUpDate { get; set; }
    }
}