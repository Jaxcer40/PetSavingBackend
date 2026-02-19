using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Appointment;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class AppointmentMappers
    {
        public static ReadAppointmentDTO ToReadAppointmentDTO(this Appointment appointmentModel)
        {
            return new ReadAppointmentDTO
            {
                Id= appointmentModel.Id,
                AppointmentDate= appointmentModel.AppointmentDate,
                Diagnosis=appointmentModel.Diagnosis,
                Treatment=appointmentModel.Treatment,
                Notes= appointmentModel.Notes,
                FollowUpDate=appointmentModel.FollowUpDate,

                
                Pet = new PetSummaryDTO
                {
                    Id= appointmentModel.Pet.Id,
                    Name = appointmentModel.Pet.Name,
                    Species = appointmentModel.Pet.Species
                },

                Vet = new VetSummaryDTO
                {
                    Id =appointmentModel.Vet.Id,
                    FirstName= appointmentModel.Vet.FirstName,
                    LastName= appointmentModel.Vet.LastName,
                    Specialization=appointmentModel.Vet.Specialization
                },

                Client = new ClientSummaryDTO
                {
                    Id=appointmentModel.Client.Id,
                    FirstName= appointmentModel.Client.FirstName,
                    LastName= appointmentModel.Client.LastName,
                }
            };
        }
        public static Appointment ToAppointmentFromCreateDTO(this CreateAppointmentDTO appointmentDTO)
        {
            return new Appointment
            {
                AppointmentDate= appointmentDTO.AppointmentDate,
                Diagnosis=appointmentDTO.Diagnosis,
                Treatment=appointmentDTO.Treatment,
                Notes= appointmentDTO.Notes,
                FollowUpDate=appointmentDTO.FollowUpDate,

                //Llaves foraneas
                ClientId= appointmentDTO.ClientId,
                PetId = appointmentDTO.PetId,
                VetId = appointmentDTO.VetId,
            };
        }
    }
}