using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Pet
{
    public class ReadPetDTO
    {
        public int Id {get; set;}

        //Llave foranea hacia client
        public ClientSummaryDTO Client {get; set; }=null!;

        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime BirthDate { get; set;  }

        public decimal Weight { get; set; }

        public DateTime AdoptedDate { get; set; }

        public int Rating { get; set; }
    }

    public class ClientSummaryDTO
    {
        public int Id {get; set;}
        public string FirstName {set; get;}= string.Empty;
        public string LastName {set; get;}= string.Empty;
    }
}