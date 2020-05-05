using System;
using System.Collections.Generic;
using Api.DTO;

namespace Api.Domain.Interfaces
{
    public interface IPartnerDomain
    {
        DTOResponse<DTOPartner> Create(DTOPartner createInfo);
        DTOResponse<List<DTOPartner>> GetAll(DTOPartnerSearch searchQuery);
        DTOResponse<DTOPartner> GetById(string id);

        DTOResponse<DTOPartner> Update(DTOPartner updateInfo);
        DTOResponse<bool> DeleteById(string id);

        DTOResponse<List<DTOPlace>> SearchPlaces(DTOPlaceSearch searchQuery);
        DTOResponse<DTOPlace> GetPlaceDetails(string placeId);
        DTOResponse<DTOAddress> GeocodeAddress(DTOAddress address);

        DTOResponse<string> Export();
    }
}
