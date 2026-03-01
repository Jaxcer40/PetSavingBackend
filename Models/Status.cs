using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PetSavingBackend.Models
{
    public class Status
    {
        public Guid Id { get; set; }

        // llave foranea hacia Admission
        public Guid AdmissionId { get; set; }
        public Admission Admission { get; set; } = null!;

        public string CurrentStatus { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

    }
}