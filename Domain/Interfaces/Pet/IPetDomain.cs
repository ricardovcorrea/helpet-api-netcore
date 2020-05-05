using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface IPetDomain
    {
        DTOResponse<List<DTOPet>> GetAllPets();
        DTOResponse<List<DTOPet>> GetPetsByUserId(string userId);
        DTOResponse<DTOPet> Create(DTOPet createPetInfo);
    }
}
