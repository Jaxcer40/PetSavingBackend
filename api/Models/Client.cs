using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        //required es para que el campo no pueda quedar vacio
        [Required (ErrorMessage ="El nombre es obligatorio")]
        //maxlenght es para definir la cantidad maxima de caracteres permitidos
        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres") ]
        public string FirstName { get; set; } = string.Empty; //string.empty es usado para evitar valores nulos

        [Required (ErrorMessage ="El apellido es obligatorio")]
        [MaxLength(100, ErrorMessage ="El apellido no puede superar los 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required (ErrorMessage ="El email es obligatorio")]
        //emailaddress es para validar que el formato del email sea correcto
        [EmailAddress (ErrorMessage ="El formato del email es incorrecto")]
        [MaxLength(150, ErrorMessage ="El email no puede superar los 150 caracteres")]
        public string Email { get; set; } = string.Empty;
        
        [Required (ErrorMessage ="El telefono es obligatorio")]
        // regularexpression es para definir un patron que debe cumplir el campo
        [RegularExpression (@"^\d+$", ErrorMessage ="El telefono solo puede contener numeros")]
        [MaxLength(15, ErrorMessage ="El telefono no puede superar los 15 caracteres")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required (ErrorMessage ="La direccion es obligatoria")]

        public string Address { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        //definir un valor por default para la fecha de registro
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        
        [Required (ErrorMessage ="El nombre del contacto de emergencia es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres") ]
        public string EmergencyContactName { get; set; } = string.Empty; 

        [Required (ErrorMessage ="El telefono del contacto de emergencia es obligatorio")]
        [RegularExpression (@"^\d+$", ErrorMessage ="El telefono solo puede contener numeros")]
        [MaxLength(15, ErrorMessage ="El telefono no puede superar los 15 caracteres")]
        public string EmergencyContactPhone { get; set; } = string.Empty;

        //relacion uno a muchos con Patient
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

        //relacion uno a muchos con Appointmet
        public ICollection<Appointmet> Appointmets { get; set; } = new List<Appointmet>();
    }
}