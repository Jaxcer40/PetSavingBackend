using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Account
{
    public class RegisterAndLoginRequestDTO
    {
        [Required]
        public required string UserName { get; set; }
        public required string Token { get; set; }
    }
}