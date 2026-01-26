using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Vet
{
    public class ReadVetDTO
    {
        public int Id {get; set;}

        public string FirstName {get;set;}=string.Empty;

        public string LastName {get;set;}=string.Empty;

        public string Email {get;set;}=string.Empty;

        public string PhoneNumber {get;set;}=string.Empty;
        
        public string Specialization {get;set;}=string.Empty;

        public DateTime BirthDate {get;set;}

        public DateTime HireDate {get;set;}

        public string Activity {get;set;}=string.Empty;
    }
}