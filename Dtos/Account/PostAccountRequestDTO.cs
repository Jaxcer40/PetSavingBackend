using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Account
{
    public class PostAccountRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "El nombre de usuario no puede ser mayor a 100 caracteres")]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(150, ErrorMessage = "El correo electronico no puede ser mayor a 150 caracteres")]
        public required string Email { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "El numero de telefono no puede ser mayor a 150 caracteres")]
        public string? PhoneNumber { get; set; }
        [MaxLength(100, ErrorMessage = "La especializacion no puede ser mayor a 100 caracteres")]
        public string Specialization { get; set; } = "Sin especializacion";
        public DateOnly BirthDate { get; set; }
        [MaxLength(50, ErrorMessage = "La actividad no puede superar los 50 caracteres")]
        public string Activity { get; set; } = string.Empty;
        [Required]
        public required string Password {get; set;}
    }
}