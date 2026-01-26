using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Client;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client clientModel);
        Task<Client?> PatchAsync(int id, UpdateClientDTO updateDTO);
        Task<Client?> DeleteAsync(int id);
    }
}
