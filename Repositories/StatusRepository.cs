using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Helper;
using PetSavingBackend.Data;
using PetSavingBackend.DTOs.Status;

namespace PetSavingBackend.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDBContext _context;
        public StatusRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Status> CreateAsync(Status statusModel)
        {
            var admissionExists = await _context.Admissions.AnyAsync(a => a.Id == statusModel.AdmissionId);
                if (!admissionExists)
            throw new ArgumentException("El AdmissionId no existe.");

            await _context.Statuses.AddAsync(statusModel);
            await _context.SaveChangesAsync();

            var statusWithAdmission = await _context.Statuses
                .Include(s=>s.Admission)
                .FirstOrDefaultAsync(s=>s.Id == statusModel.Id);
            
            return statusWithAdmission!;
        }

        public async Task<Status?> DeleteAsync(int id)
        {
            var statusModel= await _context.Statuses.FirstOrDefaultAsync(x=>x.Id==id);

            if (statusModel == null)
            {
                return null;
            }

            _context.Statuses.Remove(statusModel);
            await _context.SaveChangesAsync();
            return statusModel;
        }

        public async Task<List<Status>> GetAllAsync()
        {
            return await _context.Statuses.Include(s=>s.Admission).ToListAsync();
        }

        public async Task<Status?> GetByIdAsync(int id)
        {
            return await _context.Statuses
                .Include(s=>s.Admission)
                .FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<PagedResponse<Status>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Statuses.Include(s=>s.Admission).OrderBy(s=>s.Id).AsQueryable();

            var totalRecords =await query.CountAsync();

            var statuses= await query
                .Skip((pageNumber - 1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResponse<Status>(statuses, totalRecords, pageNumber, pageSize);
        }

        public async Task<Status?> PatchAsync(int id, UpdateStatusDTO updateDTO)
        {
            var existingStatus = await _context.Statuses.FindAsync(id);
            if (existingStatus==null) return null;

            if (updateDTO.AdmissionId.HasValue)
            {
                var admissionExists= await _context.Admissions.AnyAsync(a=>a.Id==updateDTO.AdmissionId.Value);
                if(!admissionExists)
                    throw new ArgumentException("El AdmissionId no existe.");
                
                existingStatus.AdmissionId=updateDTO.AdmissionId.Value;
            }

            if (!string.IsNullOrEmpty(updateDTO.CurrentStatus))
                existingStatus.CurrentStatus=updateDTO.CurrentStatus;
            
            if (!string.IsNullOrWhiteSpace(updateDTO.Notes))
                existingStatus.Notes=updateDTO.Notes;

            await _context.SaveChangesAsync();
            
            var statusWithAdmission=await _context.Statuses
                .Include(s=>s.Admission)
                .FirstOrDefaultAsync(s=>s.Id ==existingStatus.Id);
            
            return statusWithAdmission;
        }
    }
}