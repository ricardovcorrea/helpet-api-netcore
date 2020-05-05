using System;
namespace Api.DTO
{
    public class DTOPlace
    {
        public string Name { get; set; }
        public string FormatedAddress { get; set; }
        public string PlaceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public DTOAddress Address { get; set; }
    }
}
