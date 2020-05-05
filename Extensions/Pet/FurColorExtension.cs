using System;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class FurColorExtension
    {
        public static DTOFurColor ToDTO(this FurColorModel model)
        {
            return new DTOFurColor()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static FurColorModel ToModel(this DTOFurColor dto)
        {
            return new FurColorModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
