using System;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class BreedExtension
    {
        public static DTOBreed ToDTO(this BreedModel model)
        {
            return new DTOBreed()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static BreedModel ToModel(this DTOBreed dto)
        {
            return new BreedModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
