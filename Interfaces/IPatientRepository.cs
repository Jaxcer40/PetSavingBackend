using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Models;
using PetSavingBackend.DTOs;
using PetSavingBackend.DTOs.Patient;

namespace PetSavingBackend.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> CreateAsync(Patient patientModel);
        Task<Patient?> PatchAsync(int id, UpdatePatientDTO updateDTO);
        Task<Patient?> DeleteAsync(int id);
    }
}