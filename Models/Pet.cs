using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Pet
    {
        public int Id { get; set; }

        // llave foranea hacia Client
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set;  }

        public decimal Weight { get; set; }

        public DateTime? AdoptedDate { get; set; }

        public int Rating { get; set; }

        //relacion uno a muchos con Appointmet
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        //relacion uno a muchos con Admission
        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}