using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSavingBackend.DTOs.Status;
using PetSavingBackend.Helper;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllAsync();
        Task<PagedResponse<Status>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Status?> GetByIdAsync(Guid id);
        Task<Status> CreateAsync(Status statusModel);
        Task<Status?> PatchAsync(Guid id, UpdateStatusDTO updateDTO);
        Task<Status?> DeleteAsync(Guid id);
    }
}