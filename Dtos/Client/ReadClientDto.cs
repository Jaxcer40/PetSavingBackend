using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Client
{
    public class ReadClientDTO
    {
        public int Id {set; get;}

        public string FirstName {set; get;}=string.Empty;

        public string LastName {set; get;}=string.Empty;

        public string Email {set; get;}=string.Empty;

        public string PhoneNumber {set; get;}=string.Empty;

        public string Address {set; get;}=string.Empty;

        public DateTime BirthDate {set; get; }

        public DateTime RegistrationDate {set; get;}

        public string EmergencyContactName {set; get;}=string.Empty;

        public string EmergencyContactPhone {set; get;}=string.Empty;
    }
}