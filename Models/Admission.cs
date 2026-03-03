using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Admission
    {
        public Guid Id { get; set; }

        // llave foranea hacia Pet  
        public Guid PetId { get; set; }
        public Pet Pet { get; set; } = null!;  

        // llave foranea hacia Vet
        public Guid VetId { get; set; }
        public AppUser Vet { get; set; } = null!;

        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public string AdmissionReason { get; set; } = string.Empty;

        public string CageNumber { get; set; } = string.Empty;

        //relacion uno a muchos con Status
        public ICollection<Status> Statuses { get; set; } = new List<Status>();
    }
}