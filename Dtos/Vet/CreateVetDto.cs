using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PetSavingBackend.DTOs.Vet
{
    public class CreateVetDTO
    {
        [Required]
        public string FirstName {get;set;}=string.Empty;

        [Required]
        public string LastName {get;set;}=string.Empty;

        [Required]
        public string Email {get;set;}=string.Empty;

        [Required]
        public string PhoneNumber {get;set;}=string.Empty;
        
        [Required]
        public string Specialization {get;set;}=string.Empty;

        public DateTime BirthDate {get;set;}

        public DateTime HireDate {get;set;}
        
        public string Activity {get;set;}=string.Empty;
    }
}