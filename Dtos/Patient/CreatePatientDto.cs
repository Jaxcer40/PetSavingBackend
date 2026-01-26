using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PetSavingBackend.DTOs.Patient
{
    public class CreatePatientDTO
    {
        //Llave foranea a Client
        [Required]
       public int ClientId {get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime BirthDate { get; set;  }

        [Required]
        public decimal Weight { get; set; }

        public DateTime AdoptedDate { get; set; }
    }
}