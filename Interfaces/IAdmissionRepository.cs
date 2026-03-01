using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Models;
using PetSavingBackend.DTOs.Admission;
using PetSavingBackend.Helper; 

namespace PetSavingBackend.Interfaces
{
    public interface IAdmissionRepository
    {
        Task<List<Admission>> GetAllAsync();
        Task<PagedResponse<Admission>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Admission?> GetByIdAsync(Guid id);
        Task<Admission> CreateAsync(Admission admissionModel);
        Task<Admission?> PatchAsync(Guid id, UpdateAdmissionDTO updateDTO);
        Task<Admission?> DeleteAsync(Guid id);
    }
}