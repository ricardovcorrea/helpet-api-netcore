using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Api.DTO;
using Api.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("partners")]
    public class PartnersController : ControllerBase
    {
        private readonly ILogger<PartnersController> _logger;
        private readonly IPartnerDomain _domain;

        public PartnersController(ILogger<PartnersController> logger, IPartnerDomain partnerDomain)
        {
            _logger = logger;
            _domain = partnerDomain;
        }

        [HttpPost]
        [Route("list")]
        public DTOResponse<List<DTOPartner>> GetAll(DTOPartnerSearch searchQuery)
        {
            return _domain.GetAll(searchQuery);
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOPartner> Create(DTOPartner createInfo)
        {
            return _domain.Create(createInfo);
        }

        [HttpPost]
        [Route("detail")]
        public DTOResponse<DTOPartner> GetById(DTOPartner getByIdInfo)
        {
            return _domain.GetById(getByIdInfo.Id);
        }

        [HttpPost]
        [Route("update")]
        public DTOResponse<DTOPartner> Update(DTOPartner updateInfo)
        {
            return _domain.Update(updateInfo);
        }

        [HttpPost]
        [Route("delete")]
        public DTOResponse<bool> Delete(DTOPartner deleteByIdInfo)
        {
            return _domain.DeleteById(deleteByIdInfo.Id);
        }

        [HttpPost]
        [Route("places/search")]
        public DTOResponse<List<DTOPlace>> SearchPlaces(DTOPlaceSearch searchQuery)
        {
            return _domain.SearchPlaces(searchQuery);
        }

        [HttpPost]
        [Route("places/detail")]
        public DTOResponse<DTOPlace> GetPlaceDetail(DTOPlaceSearch searchPlaceInfo)
        {
            return _domain.GetPlaceDetails(searchPlaceInfo.PlaceId);
        }

        [HttpPost]
        [Route("places/geocode")]
        public DTOResponse<DTOAddress> GeocodePlace(DTOAddress address)
        {
            return _domain.GeocodeAddress(address);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("export")]
        public IActionResult Export()
        {
            var exportResponse = _domain.Export();
            return File(System.Text.Encoding.ASCII.GetBytes(exportResponse.Data), "text/csv", "Partners.csv");
        }
    }
}
