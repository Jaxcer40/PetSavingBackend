using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Account
{   
    // Note: This DTO might be reduntant with LoginResponseDTO
    public class RegisterAndLoginRequestDTO
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Token { get; set; }
    }
}