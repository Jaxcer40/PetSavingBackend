using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.DTOs.Admission;
using PetSavingBackend.DTOs.Pet;
using PetSavingBackend.Models;

namespace PetSavingBackend.Mappers
{
    public static class PetMappers
    {
        public static ReadPetDTO ToReadPetDTO(this Pet petModel)
        {
            return new ReadPetDTO
            {
                Id= petModel.Id,
                Name=petModel.Name,
                Species=petModel.Species,
                Breed=petModel.Breed,
                Gender=petModel.Gender,
                BirthDate=petModel.BirthDate,
                Weight=petModel.Weight,
                AdoptedDate=petModel.AdoptedDate,
                Rating = petModel.Rating,

                Client= new ClientSummaryDTO
                {
                    Id=petModel.Client.Id,
                    FirstName=petModel.Client.FirstName,
                    LastName=petModel.Client.LastName
                }
            };
        }

        public static Pet ToPetFromCreateDTO(this CreatePetDTO petDTO)
        {
            return new Pet
            {
                Name=petDTO.Name,
                Species=petDTO.Species,
                Breed=petDTO.Breed,
                Gender=petDTO.Gender,
                BirthDate=petDTO.BirthDate,
                Weight=petDTO.Weight,
                AdoptedDate=petDTO.AdoptedDate,
                Rating = petDTO.Rating,  

                //Lave foranea a Client
                ClientId= petDTO.ClientId,
            };
        }

    }
}