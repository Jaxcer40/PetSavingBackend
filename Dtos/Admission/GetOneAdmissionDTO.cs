using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Admission
{
    public class GetOneAdmissionDTO
    {
        public Guid Id { get; set; }

        // llave foranea hacia Pet
        public GetOnePetSummaryDTO Pet {get; set;}=null!;

        // llave foranea hacia Vet
        public GetOneVetSummaryDTO Vet {get; set;}=null!;

        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string AdmissionReason { get; set; } = string.Empty;
        public string CageNumber { get; set; } = string.Empty;
        public bool Discharged { get; set; }
        public List<GetOneStatusSummaryDTO>? Statuses { get; set; }
    }

    public class GetOnePetSummaryDTO
    {
        public Guid Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Species { get; set; } = string.Empty;
    }

    public class GetOneVetSummaryDTO
    {
        public Guid Id {get; set;}
        public string UserName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }

    public class GetOneStatusSummaryDTO
    {
        public Guid Id { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}