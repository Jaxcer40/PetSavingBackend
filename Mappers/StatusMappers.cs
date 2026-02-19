using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Status;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class StatusMappers
    {
        public static ReadStatusDTO ToReadStatusDTO(this Status statusModel)
        {
            return new ReadStatusDTO
            {
                Id = statusModel.Id,
                CurrentStatus = statusModel.CurrentStatus,
                Notes = statusModel.Notes,
                Admission = new AdmissionSummaryDTO
                {
                    Id=statusModel.Admission.Id,
                    AdmissionDate = statusModel.Admission.AdmissionDate,
                    AdmissionReason = statusModel.Admission.AdmissionReason
                }
            };

        }

        public static Status ToStatusFromCreateDTO(this CreateStatusDTO statusDTO)
        {
            return new Status
            {
               AdmissionId= statusDTO.AdmissionId,
               CurrentStatus= statusDTO.CurrentStatus,
                Notes=statusDTO.Notes
            };
        }
    }
}