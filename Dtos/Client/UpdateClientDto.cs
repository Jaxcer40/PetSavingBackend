using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSavingBackend.DTOs.Client
{
    public class UpdateClientDTO
    {
        //maxlenght es para definir la cantidad maxima de caracteres permitidos
        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres") ]
        public string? FirstName {set; get;}=string.Empty;

        [MaxLength(100, ErrorMessage ="El apellido no puede superar los 100 caracteres")]
        public string? LastName {set; get;}=string.Empty;

        //emailaddress es para validar que el formato del email sea correcto
        [EmailAddress (ErrorMessage ="El formato del email es incorrecto")]
        [MaxLength(150, ErrorMessage ="El email no puede superar los 150 caracteres")]
        public string? Email {set; get;}

        // regularexpression es para definir un patron que debe cumplir el campo
        [RegularExpression (@"^\d+$", ErrorMessage ="El telefono solo puede contener numeros")]
        [MaxLength(15, ErrorMessage ="El telefono no puede superar los 15 caracteres")]
        public string? PhoneNumber {set; get;}=string.Empty;

        public string? Address {set; get;}=string.Empty;

        public DateTime? BirthDate {set; get; }

        public DateTime? RegistrationDate {set; get;}

        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres") ]
        public string? EmergencyContactName {set; get;}=string.Empty;

        [RegularExpression (@"^\d+$", ErrorMessage ="El telefono solo puede contener numeros")]
        [MaxLength(15, ErrorMessage ="El telefono no puede superar los 15 caracteres")]
        public string? EmergencyContactPhone {set; get;}=string.Empty;
    }
}