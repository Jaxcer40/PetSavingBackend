using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Status;
using api.Models;

namespace api.Mappers
{
    public static class StatusMappers
    {
        public static ReadStatusDto ToReadStatusDto(this Status statusModel)
        {
            return new ReadStatusDto
            {
                Id = statusModel.Id,
                CurrentStatus = statusModel.CurrentStatus,
                Notes = statusModel.Notes,
                Admission = new AdmissionSummaryDto
                {
                    AdmissionDate = statusModel.Admission.AdmissionDate,
                    AdmissionReason = statusModel.Admission.AdmissionReason
                }
            };

        }

        public static Status ToStatusFromCreateDto(this CreateStatusDto statusDto)
        {
            return new Status
            {
               AdmissionId= statusDto.AdmissionId,
               CurrentStatus= statusDto.CurrentStatus,
                Notes=statusDto.Notes
            };
        }
    }
}