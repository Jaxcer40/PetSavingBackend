using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}