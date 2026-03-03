using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PetSavingBackend.Dtos.Account;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser?> UserExistCheckByEmailAsync(string email);
        Task<AppUser?> UserExistCheckByIdAsync(Guid id);
        Task<RegisterAndLoginRequestDTO?> RegisterNewUserAsync(AppUser registerNew, string role, string password);
        Task<List<AppUser>> GetAllAsync ();
        Task<AppUser?> GetByIdAsync (Guid id);
        Task<IdentityResult> PatchAccountAsync(Guid id, PatchAccountRequestDTO updateDTO);
        Task<IdentityResult> DeleteAsync (Guid id);
    }
}