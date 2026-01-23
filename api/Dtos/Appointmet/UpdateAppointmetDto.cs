using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Appointmet
{
    public class UpdateAppointmetDto
    {
        //llave foranea hacia Patient
        public int? PatientId {get; set;}

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