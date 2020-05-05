using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface IFurColorDomain
    {
        DTOResponse<DTOFurColor> Create(DTOFurColor createInfo);
        DTOResponse<List<DTOFurColor>> GetAll();
        DTOResponse<DTOFurColor> GetById(string id);
    }
}
