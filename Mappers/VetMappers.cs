using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Vet;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class VetMappers
    {
        public static ReadVetDTO ToReadVetDTO(this Vet vetModel)
        {
            return new ReadVetDTO
            {
                Id = vetModel.Id,
                FirstName = vetModel.FirstName,
                LastName = vetModel.LastName,
                Email = vetModel.Email,
                PhoneNumber = vetModel.PhoneNumber,
                Specialization = vetModel.Specialization,
                BirthDate = vetModel.BirthDate,
                HireDate = vetModel.HireDate,
                Activity = vetModel.Activity
            };
        }

        public static Vet ToVetFromCreateDTO(this CreateVetDTO vetDTO)
        {
            return new Vet
            {
                FirstName = vetDTO.FirstName,
                LastName = vetDTO.LastName,
                Email = vetDTO.Email,
                PhoneNumber = vetDTO.PhoneNumber,
                Specialization = vetDTO.Specialization,                
                BirthDate = vetDTO.BirthDate,
                HireDate = vetDTO.HireDate,
                Activity = vetDTO.Activity
            };
        }
    }
}