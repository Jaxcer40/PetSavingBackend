using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Status
{
    public class CreateStatusDTO
    {
        // llave foranea hacia Admission
        [Required]
        public int AdmissionId { get; set; }

        [Required]
        public string CurrentStatus { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}