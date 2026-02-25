using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetSavingBackend.DTOs.Pet
{
    public class CreatePetDTO
    {
        //Llave foranea a Client
        [Required]
        public int ClientId {get; set; }

        [Required (ErrorMessage ="El nombre de la mascota es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre de la mascota no puede superar los 100 caracteres") ]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage ="La especie no puede superar los 50 caracteres")]
        public string Species { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage ="La raza no puede superar los 100 caracteres")]
        public string Breed { get; set; } = string.Empty;

        [MaxLength(10, ErrorMessage ="El genero no puede superar los 10 caracteres")]
        public string Gender { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set;  }

        // definir el tipo de dato decimal con precision y escala
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }

        public DateTime? AdoptedDate { get; set; }

        public int Rating { get; set; }
    }
}