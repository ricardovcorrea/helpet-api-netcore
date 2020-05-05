using System;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class PetExtension
    {
        public static DTOPet ToDTO(this PetModel model)
        {
            return new DTOPet()
            {
                Id = model.Id,
                UserId = model.UserId,
                Type = (PetType)model.Type,
                Name = model.Name,
                Weight = model.Weight,
                Birthdate = model.Birthdate,
                DistinctiveFeature = model.DistinctiveFeature,
                MicrochipOrTattoNumber = model.MicrochipOrTattoNumber,
                MicrochipOrTattoApplyDate = model.MicrochipOrTattoApplyDate,
                GpsColarId = model.GpsColarId,
                Gender = (Gender)model.Gender,
                DocumentFrontImage = new DTOFile() { Id = model.DocumentFrontImageId },
                Image = new DTOFile() { Id = model.ImageId },
                Breed = new DTOBreed() { Id = model.BreedId },
                Coat = new DTOCoat() { Id = model.CoatId },
                FurColor = new DTOFurColor() { Id = model.FurColorId }
            };
        }

        public static PetModel ToModel(this DTOPet dto)
        {
            return new PetModel()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Type = (int)dto.Type,
                Name = dto.Name,
                Weight = dto.Weight,
                Birthdate = dto.Birthdate,
                DistinctiveFeature = dto.DistinctiveFeature,
                MicrochipOrTattoNumber = dto.MicrochipOrTattoNumber,
                MicrochipOrTattoApplyDate = dto.MicrochipOrTattoApplyDate,
                GpsColarId = dto.GpsColarId,
                Gender = (int)dto.Gender,
                DocumentFrontImageId = dto.DocumentFrontImage?.Id,
                ImageId = dto.Image?.Id,
                BreedId = dto.Breed?.Id,
                CoatId = dto.Coat?.Id,
                FurColorId = dto.FurColor?.Id
            };
        }
    }
}
