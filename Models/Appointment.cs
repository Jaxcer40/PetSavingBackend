using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }

        // llave foranea hacia Pet
        public Guid PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        // llave foranea hacia Client
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;

        // llave foranea hacia Vet
        public Guid VetId { get; set; }
        public Vet Vet { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }
}