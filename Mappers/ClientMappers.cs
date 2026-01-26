using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Client;
using PetSavingBackend.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PetSavingBackend.Mappers
{
    public static class ClientMappers
    {
        public static ReadClientDTO ToReadClientDTO(this Client clientModel)
        {
            return new ReadClientDTO
            {
                Id= clientModel.Id,
                FirstName=clientModel.FirstName,
                LastName=clientModel.LastName,
                Email=clientModel.Email,
                PhoneNumber=clientModel.PhoneNumber,
                Address=clientModel.Address,
                BirthDate=clientModel.BirthDate,
                EmergencyContactName=clientModel.EmergencyContactName,
                EmergencyContactPhone=clientModel.EmergencyContactPhone,
            };
        }

        public static Client ToClientFromCreateDTO(this CreateClientDTO clientDTO)
        {
            return new Client
            {
                FirstName=clientDTO.FirstName,
                LastName=clientDTO.LastName,
                Email=clientDTO.Email,
                PhoneNumber=clientDTO.PhoneNumber,
                Address=clientDTO.Address,
                BirthDate=clientDTO.BirthDate,
                EmergencyContactName=clientDTO.EmergencyContactName,
                EmergencyContactPhone=clientDTO.EmergencyContactPhone,
            };
        }
    }
}