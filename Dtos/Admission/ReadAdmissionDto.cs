using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Admission
{
    public class ReadAdmissionDTO
    {
        public int Id { get; set; }

        // llave foranea hacia Pet
        public PetSummaryDTO Pet {get; set;}=null!;

        // llave foranea hacia Vet
        public VetSummaryDTO Vet {get; set;}=null!;

        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string AdmissionReason { get; set; } = string.Empty;

        public string CageNumber { get; set; } = string.Empty;


    }

    public class PetSummaryDTO
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Species { get; set; } = string.Empty;

    }

    public class VetSummaryDTO
    {
        public int Id {get; set;}
        public string FirstName { get; set; } = string.Empty;
        public string LastName {get;set;}= string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }

}