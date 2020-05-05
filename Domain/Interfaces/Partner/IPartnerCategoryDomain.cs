using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface IPartnerCategoryDomain
    {
        DTOResponse<DTOPartnerCategory> Create(DTOPartnerCategory createInfo);

        DTOResponse<List<DTOPartnerCategory>> GetAll();

        DTOResponse<DTOPartnerCategory> GetById(string id);
        DTOResponse<List<DTOPartnerCategory>> GetByIdList(List<string> idsList);

        DTOResponse<DTOPartnerCategory> Update(DTOPartnerCategory updateInfo);

        DTOResponse<bool> DeleteById(string id);
    }
}
