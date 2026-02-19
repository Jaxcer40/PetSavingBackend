using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSavingBackend.DTOs.Appointment
{
    public class ReadAppointmentDTO
    {
        public int Id { get; set; }

        //llave foranea hacia Pet
        public PetSummaryDTO Pet {get; set;}=null!;

        //llave foranea hacia Client
        public ClientSummaryDTO Client {get; set;}=null!;
        
        //llave foranea hacia Vet
        public VetSummaryDTO Vet {get; set;}=null!;

        public DateTime AppointmentDate { get; set; }

        public string Diagnosis { get; set; } = string.Empty;

        public string Treatment { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateOnly FollowUpDate { get; set; }
    }

    public class PetSummaryDTO
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Species { get; set; } = string.Empty;
    }

    public class ClientSummaryDTO
    {
        public int Id {get; set;}
        public string FirstName {set; get;}= string.Empty;

        public string LastName {set; get;}= string.Empty;
    }

    public class VetSummaryDTO
    {
        public int Id {get; set;}
        public string FirstName {set; get;}= string.Empty;

        public string LastName {set; get;}= string.Empty;

        public string Specialization {set; get;}=string.Empty;
    }
}