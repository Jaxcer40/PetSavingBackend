using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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

                Patient = new PatientSummaryDTO
                {
                    Name = admissionModel.Patient.Name,
                    Species = admissionModel.Patient.Species
                },

                Vet= new VetSummaryDTO
                {
                    FirstName=admissionModel.Vet.FirstName,
                    LastName=admissionModel.Vet.LastName,
                    Specialization=admissionModel.Vet.Specialization
                }
          
            };
        }

        public static Admission ToAdmissionFromCreateDTO(this CreateAdmissionDTO admissionDTO)
        {
            return new Admission
            {
                AdmissionDate = admissionDTO.AdmissionDate,
                DischargeDate = admissionDTO.DischargeDate,
                AdmissionReason = admissionDTO.AdmissionReason,
                CageNumber = admissionDTO.CageNumber,

                // Aquí simplemente asignas los Ids
                //Son llaves foraneas 
                PatientId = admissionDTO.PatientId,
                VetId = admissionDTO.VetId

            };
        }
    }
}