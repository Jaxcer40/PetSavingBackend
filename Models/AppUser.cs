using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PetSavingBackend.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Specialization { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public string Activity { get; set; } = string.Empty;

        //Relacion uno a muchos con Appointment
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}