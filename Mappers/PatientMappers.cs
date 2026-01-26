using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Admission;
using PetSavingBackend.DTOs.Patient;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class PatientMappers
    {
        public static ReadPatientDTO ToReadPatientDTO(this Patient patientModel)
        {
            return new ReadPatientDTO
            {
                Id= patientModel.Id,
                Name=patientModel.Name,
                Species=patientModel.Species,
                Breed=patientModel.Breed,
                Gender=patientModel.Gender,
                BirthDate=patientModel.BirthDate,
                Weight=patientModel.Weight,
                AdoptedDate=patientModel.AdoptedDate,

                Client= new ClientSummaryDTO
                {
                    FirstName=patientModel.Client.FirstName,
                    LastName=patientModel.Client.LastName
                }
            };
        }

        public static Patient ToPatientFromCreateDTO(this CreatePatientDTO patientDTO)
        {
            return new Patient
            {
                Name=patientDTO.Name,
                Species=patientDTO.Species,
                Breed=patientDTO.Breed,
                Gender=patientDTO.Gender,
                BirthDate=patientDTO.BirthDate,
                Weight=patientDTO.Weight,
                AdoptedDate=patientDTO.AdoptedDate,  

                //Lave foranea a Client
                ClientId= patientDTO.ClientId,
            };
        }

    }
}