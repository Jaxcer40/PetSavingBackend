using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PetSavingBackend.Models
{
    public class Vet
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }= DateTime.Now;

        public string Activity { get; set; } = string.Empty;

        //relacion uno a muchos con Appointmet 
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        //relacion uno a muchos con Admission
        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}