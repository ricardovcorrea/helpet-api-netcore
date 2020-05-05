using System;
using Api.DTO;
using Api.enumerators;
using Api.Models;

namespace Api.Extensions
{
    public static class PartnerCategoryExtension
    {
        public static DTOPartnerCategory ToDTO(this PartnerCategoryModel model)
        {
            return new DTOPartnerCategory()
            {
                Id = model.Id,
                Name = model.Name,
                ParentId = model.ParentId
            };
        }

        public static PartnerCategoryModel ToModel(this DTOPartnerCategory dto)
        {
            return new PartnerCategoryModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                ParentId = dto.ParentId
            };
        }
    }
}
