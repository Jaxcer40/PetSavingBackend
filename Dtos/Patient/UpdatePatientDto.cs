using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Patient
{
    public class UpdatePatientDTO
    {
        //Llave foranea a Client
        public int? ClientId {get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Species { get; set; } = string.Empty;

        public string? Breed { get; set; } = string.Empty;

        public string? Gender { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set;  }

        public decimal? Weight { get; set; }

        public DateTime? AdoptedDate { get; set; }
    }
}