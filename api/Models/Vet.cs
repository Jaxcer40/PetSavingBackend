using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace api.Models
{
    public class Vet
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres") ]
        public string FirstName { get; set; } = string.Empty;

        [Required (ErrorMessage ="El apellido es obligatorio")]
        [MaxLength(100, ErrorMessage ="El apellido no puede superar los 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required (ErrorMessage ="El email es obligatorio")]
        [EmailAddress (ErrorMessage ="El formato del email es incorrecto")]
        [MaxLength(150, ErrorMessage ="El email no puede superar los 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required (ErrorMessage ="El telefono es obligatorio")]
        [RegularExpression (@"^\d+$", ErrorMessage ="El telefono solo puede contener numeros")]
        [MaxLength(15, ErrorMessage ="El telefono no puede superar los 15 caracteres")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required (ErrorMessage ="La especializacion es obligatoria")]
        [MaxLength(100, ErrorMessage ="La especializacion no puede superar los 100 caracteres")]
        public string Specialization { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }= DateTime.Now;

        [Required (ErrorMessage ="La actividad es obligatoria")]
        [MaxLength(50, ErrorMessage ="La actividad no puede superar los 50 caracteres")]
        public string Activity { get; set; } = string.Empty;

        //relacion uno a muchos con Appointmet 
        public ICollection<Appointmet> Appointmets { get; set; } = new List<Appointmet>();

        //relacion uno a muchos con Admission
        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}