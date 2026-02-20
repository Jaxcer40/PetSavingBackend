using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;
using PetSavingBackend.DTOs.Admission;
using PetSavingBackend.Helper;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Data;

namespace PetSavingBackend.Repositories
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly ApplicationDBContext _context;
        public AdmissionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Admission> CreateAsync(Admission admissionModel)
        {
            await _context.Admissions.AddAsync(admissionModel);
            await _context.SaveChangesAsync();
            
            var admissionWithDetails = await _context.Admissions
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.Id == admissionModel.Id);
            
            return admissionWithDetails!;
        }

        public async Task<Admission?> DeleteAsync(int id)
        {
            var admissionModel = await _context.Admissions.FirstOrDefaultAsync(x => x.Id == id);

            if (admissionModel == null)
            {
                return null;
            }

            _context.Admissions.Remove(admissionModel);
            await _context.SaveChangesAsync();
            return admissionModel;
        }

        public Task<List<Admission>> GetAllAsync()
        {
            return _context.Admissions
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .ToListAsync();
        }

        public Task<Admission?> GetByIdAsync(int id)
        {
            return _context.Admissions
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<PagedResponse<Admission>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query= _context.Admissions
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .OrderBy(a => a.Id)
                .AsQueryable();
            
            var totalRecords= await query.CountAsync();

            var admissions = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResponse<Admission>(admissions, totalRecords, pageNumber, pageSize);
        }

        public async Task<Admission?> PatchAsync(int id, UpdateAdmissionDTO updateDTO)
        {
            var existingAdmission=await _context.Admissions.FindAsync(id);
            if (existingAdmission == null) return null;

            if (updateDTO.PetId.HasValue)
            {
                var petExists = await _context.Pets.AnyAsync(p => p.Id == updateDTO.PetId.Value);
                if (!petExists)
                    throw new ArgumentException("El PetId no existe.");

                existingAdmission.PetId = updateDTO.PetId.Value;
            }

            if (updateDTO.VetId.HasValue)
            {
                var vetExists = await _context.Vets.AnyAsync(v => v.Id == updateDTO.VetId.Value);
                if (!vetExists)
                    throw new ArgumentException("El VetId no existe.");

                existingAdmission.VetId = updateDTO.VetId.Value;
            }

            if (updateDTO.AdmissionDate.HasValue)
                existingAdmission.AdmissionDate = updateDTO.AdmissionDate.Value;

            if (updateDTO.DischargeDate.HasValue)
                existingAdmission.DischargeDate = updateDTO.DischargeDate.Value;

            if(!string.IsNullOrWhiteSpace(updateDTO.AdmissionReason))
                existingAdmission.AdmissionReason=updateDTO.AdmissionReason;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.CageNumber))
                existingAdmission.CageNumber=updateDTO.CageNumber;

            await _context.SaveChangesAsync();

            var admissionWithPetAndVet = await _context.Admissions
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .FirstOrDefaultAsync(a => a.Id == existingAdmission.Id);
            
            return admissionWithPetAndVet;
        }
    }
}