using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface IBreedDomain
    {
        DTOResponse<DTOBreed> Create(DTOBreed createInfo);
        DTOResponse<List<DTOBreed>> GetAll();
        DTOResponse<DTOBreed> GetById(string id);

        DTOResponse<DTOBreed> Update(DTOBreed updateInfo);
        DTOResponse<bool> DeleteById(string id);
    }
}
