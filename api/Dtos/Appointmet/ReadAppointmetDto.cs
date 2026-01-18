using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Appointmet
{
    public class ReadAppointmetDto
    {
        public int Id { get; set; }

        //llave foranea hacia Patient
        public PatientSummaryDto Patient {get; set;}=null!;

        //llave foranea hacia Client
        public ClientSummaryDto Client {get; set;}=null!;
        
        //llave foranea hacia Vet
        public VetSummaryDto Vet {get; set;}=null!;

         public DateTime AppointmentDate { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }

    public class PatientSummaryDto
    {
        public string Name {get; set;} = string.Empty;
        public string Species { get; set; } = string.Empty;
    }

    public class ClientSummaryDto
    {
        public string FirtsName {set; get;}= string.Empty;

        public string LastName {set; get;}= string.Empty;
    }

    public class VetSummaryDto
    {
        public string FirtsName {set; get;}= string.Empty;

        public string LastName {set; get;}= string.Empty;
    }
}