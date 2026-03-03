using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Account
{
    public class PatchAccountRequestDTO
    {
        [MaxLength(100, ErrorMessage = "El nombre de usuario no puede ser mayor a 100 caracteres")]
        public string? UserName { get; set; }
        [MaxLength(150, ErrorMessage ="El correo electronico no puede tener mas de 150 caracteres")]
        public string? Email { get; set; }
        [MaxLength(150, ErrorMessage ="El numero telefonico no puede tener mas de 150 caracteres")]
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public DateOnly? BirthDate { get; set; }
        [MaxLength(50, ErrorMessage ="La actividad no puede superar los 50 caracteres")]
        public string? Activity { get; set; }
        public string? Password { get; set; }
    
    }
}