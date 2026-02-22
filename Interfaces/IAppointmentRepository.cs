using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Appointment;
using PetSavingBackend.DTOs.Pet;
using PetSavingBackend.Helper;
using PetSavingBackend.Models;

namespace PetSavingBackend.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<PagedResponse<Appointment>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> CreateAsync(Appointment appointmentModel);
        Task<Appointment?> PatchAsync(int id, UpdateAppointmentDTO updateDTO);
        Task<Appointment?> DeleteAsync(int id);
    }
}