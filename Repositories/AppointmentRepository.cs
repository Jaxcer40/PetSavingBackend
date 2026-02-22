using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Models;
using PetSavingBackend.Helper;
using PetSavingBackend.Data;
using PetSavingBackend.DTOs.Appointment;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetSavingBackend.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDBContext _context;
        public AppointmentRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Appointment> CreateAsync(Appointment appointmentModel)
        {
            await _context.Appointments.AddAsync(appointmentModel);
            await _context.SaveChangesAsync();

            var appointmentWithPetAndClientAndVet= await _context.Appointments
                .Include(a=>a.Pet)
                .Include(a=>a.Client)
                .Include(a=>a.Vet)
                .FirstOrDefaultAsync(a=>a.Id ==appointmentModel.Id);
            
            return appointmentWithPetAndClientAndVet!;
        }

        public async Task<Appointment?> DeleteAsync(int id)
        {
            var appointmentModel=await _context.Appointments.FirstOrDefaultAsync(x=>x.Id==id);

            if (appointmentModel == null)
            {
                return null;
            }

            _context.Appointments.Remove(appointmentModel);
            await _context.SaveChangesAsync();
            return appointmentModel;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a=>a.Pet)
                .Include(a=>a.Client)
                .Include(a=>a.Vet)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a=>a.Pet)
                .Include(a=>a.Client)
                .Include(a=>a.Vet)
                .FirstOrDefaultAsync(a=>a.Id==id);
        }

        public async Task<PagedResponse<Appointment>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Appointments
                .Include(a=>a.Pet)
                .Include(a=>a.Client)
                .Include(a=>a.Vet)
                .OrderBy(a=>a.Id).AsQueryable();
            
            var totalRecords = await query.CountAsync();

            var appointments = await query
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
        
            return new PagedResponse<Appointment>(appointments, totalRecords, pageNumber,pageSize);
        }

        public async Task<Appointment?> PatchAsync(int id, UpdateAppointmentDTO updateDTO)
        {
            var existingAppointment=await _context.Appointments.FindAsync(id);
            if (existingAppointment==null) return null;

            if (updateDTO.PetId.HasValue)
            {
                var petExists=await _context.Pets.AnyAsync(p=>p.Id==updateDTO.PetId.Value);
                if (!petExists)
                    throw new ArgumentException("El PetId no existe");

                existingAppointment.PetId=updateDTO.PetId.Value;
            }

            if (updateDTO.ClientId.HasValue)
            {
                var clientExists=await _context.Clients.AnyAsync(c=>c.Id==updateDTO.ClientId.Value);
                if (!clientExists)
                    throw new ArgumentException("El ClientId no existe");

                existingAppointment.ClientId=updateDTO.ClientId.Value;
            }

            if (updateDTO.VetId.HasValue)
            {
                var vetExists=await _context.Vets.AnyAsync(v=>v.Id==updateDTO.VetId.Value);
                if (!vetExists)
                    throw new ArgumentException("El VetId no existe");

                existingAppointment.VetId=updateDTO.VetId.Value;
            }

            if(updateDTO.AppointmentDate.HasValue)
                existingAppointment.AppointmentDate=updateDTO.AppointmentDate.Value;

            if(!string.IsNullOrWhiteSpace(updateDTO.Diagnosis))
                existingAppointment.Diagnosis=updateDTO.Diagnosis;

            if(!string.IsNullOrWhiteSpace(updateDTO.Treatment))
                existingAppointment.Treatment=updateDTO.Treatment;
            
            if(!string.IsNullOrWhiteSpace(updateDTO.Notes))
                existingAppointment.Notes=updateDTO.Notes;

            if(updateDTO.FollowUpDate.HasValue)
                existingAppointment.FollowUpDate=updateDTO.FollowUpDate.Value;

            var appointmentWhithPetAndClientAndVet=await _context.Appointments
                .Include(a=>a.Pet)
                .Include(a=>a.Client)
                .Include(a=>a.Vet)
                .FirstOrDefaultAsync(a=>a.Id == existingAppointment.Id);

            return existingAppointment;
        }
    }
}