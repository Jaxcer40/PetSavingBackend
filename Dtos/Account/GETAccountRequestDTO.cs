using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.Dtos.Account
{
    public class GETAccountRequestDTO
    {
        public required Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Specialization { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required DateTime HireDate { get; set; }
        public required string Activity { get; set; }
    }
}
