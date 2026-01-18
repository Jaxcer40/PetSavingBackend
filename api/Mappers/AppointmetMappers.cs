using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Appointmet;
using api.Models;

namespace api.Mappers
{
    public static class AppointmetMappers
    {
        public static ReadAppointmetDto ToReadAppointmetDto(this Appointmet appointmetModel)
        {
            return new ReadAppointmetDto
            {
                Id= appointmetModel.Id,
                AppointmentDate= appointmetModel.AppointmentDate,
                Diagnosis=appointmetModel.Diagnosis,
                Treatment=appointmetModel.Treatment,
                Notes= appointmetModel.Notes,
                FollowUpDate=appointmetModel.FollowUpDate,

                
                Patient = new PatientSummaryDto
                {
                    Name = appointmetModel.Patient.Name,
                    Species = appointmetModel.Patient.Species

                },

                //AGREGAR CUANDO ESTÉ VET

                // Vet = new VetSummaryDto
                // {
                //     FirstName= appointmetModel.Vet.FirstName,
                //     LastName= appointmetModel.Vet.LastName
                // },

                //AGREGAR CUANDO ESTÉ Client

                // Vet = new VetSummaryDto
                // {
                //     FirstName= appointmetModel.Vet.FirstName,
                //     LastName= appointmetModel.Vet.LastName
                // }



            };

        }
    }
}