using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Appointmet;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class AppointmetMappers
    {
        public static ReadAppointmetDTO ToReadAppointmetDTO(this Appointmet appointmetModel)
        {
            return new ReadAppointmetDTO
            {
                Id= appointmetModel.Id,
                AppointmentDate= appointmetModel.AppointmentDate,
                Diagnosis=appointmetModel.Diagnosis,
                Treatment=appointmetModel.Treatment,
                Notes= appointmetModel.Notes,
                FollowUpDate=appointmetModel.FollowUpDate,

                
                Patient = new PatientSummaryDTO
                {
                    Name = appointmetModel.Patient.Name,
                    Species = appointmetModel.Patient.Species

                },

                Vet = new VetSummaryDTO
                {
                    FirstName= appointmetModel.Vet.FirstName,
                    LastName= appointmetModel.Vet.LastName,
                    Specialization=appointmetModel.Vet.Specialization
                },

                Client = new ClientSummaryDTO
                {
                    FirstName= appointmetModel.Client.FirstName,
                    LastName= appointmetModel.Client.LastName,
                }
            };
        }
        public static Appointmet ToAppointmetFromCreateDTO(this CreateAppointmetDTO appointmetDTO)
        {
            return new Appointmet
            {
                AppointmentDate= appointmetDTO.AppointmentDate,
                Diagnosis=appointmetDTO.Diagnosis,
                Treatment=appointmetDTO.Treatment,
                Notes= appointmetDTO.Notes,
                FollowUpDate=appointmetDTO.FollowUpDate,

                    //Llaves foraneas
                ClientId= appointmetDTO.PatientId,
                PatientId = appointmetDTO.PatientId,
                VetId = appointmetDTO.VetId,
            };
        }
    }
}