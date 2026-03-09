using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PetSavingBackend.Dtos.Admission;
using PetSavingBackend.DTOs.Admission;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class AdmissionMappers
    {
        public static ReadAdmissionDTO ToReadAdmissionDTO(this Admission admissionModel)
        {
            return new ReadAdmissionDTO
            {
                Id = admissionModel.Id,
                AdmissionDate= admissionModel.AdmissionDate,
                DischargeDate= admissionModel.DischargeDate,
                AdmissionReason= admissionModel.AdmissionReason,
                CageNumber= admissionModel.CageNumber,

                Pet = new PetSummaryDTO
                {
                    Id= admissionModel.Pet.Id,
                    Name = admissionModel.Pet.Name,
                    Species = admissionModel.Pet.Species
                },

                Vet= new VetSummaryDTO
                {   
                    Id=admissionModel.Vet.Id,
                    UserName=admissionModel.Vet.UserName,
                    Specialization=admissionModel.Vet.Specialization
                }
          
            };
        }

        public static GetOneAdmissionDTO ToGetOneAdmissionDTOFromAdmission(this Admission admissionModel)
        {
            return new GetOneAdmissionDTO
            {
                Id = admissionModel.Id,
                
                Pet = new GetOnePetSummaryDTO
                {
                    Id= admissionModel.Pet.Id,
                    Name = admissionModel.Pet.Name,
                    Species = admissionModel.Pet.Species
                },

                Vet= new GetOneVetSummaryDTO
                {   
                    Id=admissionModel.Vet.Id,
                    UserName=admissionModel.Vet.UserName,
                    Specialization=admissionModel.Vet.Specialization
                },

                AdmissionDate= admissionModel.AdmissionDate,
                DischargeDate= admissionModel.DischargeDate,
                AdmissionReason= admissionModel.AdmissionReason,
                CageNumber= admissionModel.CageNumber,
                Statuses = admissionModel.Statuses.Select(s => new GetOneStatusSummaryDTO
                {
                    Id = s.Id,
                    CurrentStatus = s.CurrentStatus,
                    Notes = s.Notes
                }).ToList()
            };
        }

        public static Admission ToAdmissionFromCreateDTO(this CreateAdmissionDTO admissionDTO)
        {
            return new Admission
            {
                DischargeDate = admissionDTO.DischargeDate,
                AdmissionReason = admissionDTO.AdmissionReason,
                CageNumber = admissionDTO.CageNumber,

                // Aquí simplemente asignas los Ids
                //Son llaves foraneas 
                PetId = admissionDTO.PetId,
                VetId = admissionDTO.VetId

            };
        }
    }
}