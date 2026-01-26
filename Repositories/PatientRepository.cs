using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Data;
using PetSavingBackend.Interfaces;
using PetSavingBackend.DTOs;
using PetSavingBackend.DTOs.Patient;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Mappers;
using PetSavingBackend.Models;

namespace PetSavingBackend.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDBContext _context;
        public PatientRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Patient> CreateAsync(Patient patientModel)
        {
            await _context.Patients.AddAsync(patientModel);
            await _context.SaveChangesAsync();
            return patientModel;
        }

        public async Task<Patient?> DeleteAsync(int id)
        {
            var patientModel= await _context.Patients.FirstOrDefaultAsync(x=>x.Id==id);

            if (patientModel == null)
            {
                return null;
            }

            _context.Patients.Remove(patientModel);
            await _context.SaveChangesAsync();
            return patientModel;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.Include(p => p.Client).ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<Patient?> PatchAsync(int id, UpdatePatientDTO updateDTO)
        {
            var existingPatient=await _context.Patients.FindAsync(id);
            if (existingPatient==null) return null;
            
            if(updateDTO.ClientId.HasValue)
            {
                var clientExists = await _context.Clients.AnyAsync(c => c.Id == updateDTO.ClientId.Value);
                if (!clientExists)
                    throw new ArgumentException("El ClientId no existe.");

                existingPatient.ClientId=updateDTO.ClientId.Value;
            }

            if(!string.IsNullOrWhiteSpace(updateDTO.Name))
                existingPatient.Name=updateDTO.Name;
            
            if (!string.IsNullOrWhiteSpace(updateDTO.Species))
                existingPatient.Species=updateDTO.Species;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Breed))
                existingPatient.Breed=updateDTO.Breed;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Gender))
                existingPatient.Gender=updateDTO.Gender;
            
            if (updateDTO.BirthDate.HasValue && updateDTO.BirthDate.Value > DateTime.UtcNow)
                throw new ArgumentException("La fecha de nacimiento no puede ser futura");


            if(updateDTO.BirthDate.HasValue)
                existingPatient.BirthDate=updateDTO.BirthDate.Value;
            
            if (updateDTO.Weight.HasValue && updateDTO.Weight.Value < 0)
                throw new ArgumentException("El peso no puede ser menor a 0");

            if(updateDTO.Weight.HasValue)
                existingPatient.Weight=updateDTO.Weight.Value;

            if(updateDTO.AdoptedDate.HasValue)
                existingPatient.AdoptedDate=updateDTO.AdoptedDate.Value;

            await _context.SaveChangesAsync();
            return existingPatient;
        }
    }
}