using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace api.Models
{
    public class Patient
    {
        public int Id { get; set; }

        // llave foranea hacia Client
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        [Required (ErrorMessage ="El nombre de la mascota es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre de la mascota no puede superar los 100 caracteres") ]
        public string Name { get; set; } = string.Empty;

        [Required (ErrorMessage ="La especie es obligatoria")]
        [MaxLength(50, ErrorMessage ="La especie no puede superar los 50 caracteres")]
        public string Species { get; set; } = string.Empty;

        [Required (ErrorMessage ="La raza es obligatoria")]
        [MaxLength(100, ErrorMessage ="La raza no puede superar los 100 caracteres")]
        public string Breed { get; set; } = string.Empty;

        [Required (ErrorMessage ="El genero es obligatorio")]
        [MaxLength(10, ErrorMessage ="El genero no puede superar los 10 caracteres")]
        public string Gender { get; set; } = string.Empty;

        public DateTime BirthDate { get; set;  }

        [Required (ErrorMessage ="El peso es obligatorio")]
        // definir el tipo de dato decimal con precision y escala
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }

        public DateTime AdoptedDate { get; set; }

        //relacion uno a muchos con Appointmet
        public ICollection<Appointmet> Appointmets { get; set; } = new List<Appointmet>();

        //relacion uno a muchos con Admission
        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}