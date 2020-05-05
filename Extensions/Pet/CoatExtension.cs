using System;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class CoatExtension
    {
        public static DTOCoat ToDTO(this CoatModel model)
        {
            return new DTOCoat()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static CoatModel ToModel(this DTOCoat dto)
        {
            return new CoatModel()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
