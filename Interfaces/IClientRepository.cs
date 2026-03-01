using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Client;
using PetSavingBackend.Helper;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<PagedResponse<Client>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Client?> GetByIdAsync(Guid id);
        Task<Client> CreateAsync(Client clientModel);
        Task<Client?> PatchAsync(Guid id, UpdateClientDTO updateDTO);
        Task<Client?> DeleteAsync(Guid id);
    }
}
