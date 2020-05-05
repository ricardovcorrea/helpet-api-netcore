using System;
namespace Api.DTO
{
    public class DTOPlaceSearch
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string SearchTerms { get; set; }
        public string PageToken { get; set; }
        public string PlaceId { get; set; }
    }
}
