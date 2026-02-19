using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; } = string.Empty; //string.empty es usado para evitar valores nulos

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        //definir un valor por default para la fecha de registro
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public string EmergencyContactName { get; set; } = string.Empty; 

        
        public string EmergencyContactPhone { get; set; } = string.Empty;

        //relacion uno a muchos con Pet
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();

        //relacion uno a muchos con Appointmet
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}