using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PetSavingBackend.Data;
using PetSavingBackend.DTOs.Client;
using PetSavingBackend.Helper;
using PetSavingBackend.Interfaces;
using PetSavingBackend.Models;

namespace PetSavingBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDBContext _context;
        public ClientRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Client> CreateAsync(Client clientModel)
        {
            await _context.Clients.AddAsync(clientModel);
            await _context.SaveChangesAsync();
            return clientModel;
        }

        public async Task<Client?> DeleteAsync(int id)
        {
            var clientModel = await _context.Clients.FirstOrDefaultAsync(x=>x.Id==id);

            if (clientModel == null)
            {
                return null;
            }

            _context.Clients.Remove(clientModel);
            await _context.SaveChangesAsync();
            return clientModel;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<PagedResponse<Client>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Clients.OrderBy(c => c.Id).AsQueryable();

            var totalRecords = await query.CountAsync();

            var clients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Client>(clients, totalRecords, pageNumber, pageSize);
        }

        public async Task<Client?> PatchAsync(int id, UpdateClientDTO updateDTO)
        {
            var existingClient = await _context.Clients.FindAsync(id);
            if (existingClient == null) return null;

            if (!string.IsNullOrEmpty(updateDTO.FirstName))
                existingClient.FirstName = updateDTO.FirstName;

            if (!string.IsNullOrEmpty(updateDTO.LastName))
                existingClient.LastName = updateDTO.LastName;

            if (!string.IsNullOrEmpty(updateDTO.PhoneNumber))
                existingClient.PhoneNumber = updateDTO.PhoneNumber;

            if (!string.IsNullOrEmpty(updateDTO.Address))
                existingClient.Address = updateDTO.Address;

            if (updateDTO.BirthDate.HasValue)
                existingClient.BirthDate = updateDTO.BirthDate.Value;

            if (updateDTO.RegistrationDate.HasValue)
                existingClient.RegistrationDate = updateDTO.RegistrationDate.Value;

            if (!string.IsNullOrEmpty(updateDTO.EmergencyContactName))
                existingClient.EmergencyContactName = updateDTO.EmergencyContactName;

            if (!string.IsNullOrEmpty(updateDTO.EmergencyContactPhone))
                existingClient.EmergencyContactPhone = updateDTO.EmergencyContactPhone;

            await _context.SaveChangesAsync();
            
            return existingClient;
        }
    }
}