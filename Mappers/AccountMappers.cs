using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Dtos.Account;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class AccountMappers
    {
        public static GETAccountRequestDTO ToGETAccountRequestDTOFromAppUser(this AppUser user)
        {
            return new GETAccountRequestDTO
            {
                Id = user.Id,
                UserName = user.UserName,
               Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Specialization= user.Specialization,
                BirthDate = user.BirthDate,
                HireDate = user.HireDate,
                Activity = user.Activity
            };
        }
        
    }
}