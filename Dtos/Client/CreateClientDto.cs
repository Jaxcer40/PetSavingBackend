using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSavingBackend.DTOs.Client
{
    public class CreateClientDTO
    {
        [Required]
        public string FirstName {set; get;}=string.Empty;

        [Required]
        public string LastName {set; get;}=string.Empty;

        [Required]
        public string Email {set; get;}=string.Empty;

        [Required]
        public string PhoneNumber {set; get;}=string.Empty;

        public string Address {set; get;}=string.Empty;

        public DateTime BirthDate {set; get; }

        [Required]
        public DateTime RegistrationDate {set; get;}

        public string EmergencyContactName {set; get;}=string.Empty;

        public string EmergencyContactPhone {set; get;}=string.Empty;
    }
}