using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Status
{
    public class ReadStatusDTO
    {
        public int Id { get; set; }

        // llave foranea hacia Admission
        public AdmissionSummaryDTO Admission {set; get;}=null!;

        public string CurrentStatus { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }

    public class AdmissionSummaryDTO
    {
        public int Id {get; set;}
        public DateTime AdmissionDate { get; set; }
        public string AdmissionReason { get; set; } = string.Empty;
        
    }

}