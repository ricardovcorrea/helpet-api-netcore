using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface ICoatDomain
    {
        DTOResponse<DTOCoat> Create(DTOCoat createInfo);
        DTOResponse<List<DTOCoat>> GetAll();
        DTOResponse<DTOCoat> GetById(string id);

        DTOResponse<DTOCoat> Update(DTOCoat updateInfo);
        DTOResponse<bool> DeleteById(string id);

    }
}
