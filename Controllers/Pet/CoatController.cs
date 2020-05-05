using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Api.DTO;
using Api.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("coat")]
    public class CoatController : ControllerBase
    {
        private readonly ILogger<CoatController> _logger;
        private readonly ICoatDomain _coatDomain;

        public CoatController(ILogger<CoatController> logger, ICoatDomain coatDomain)
        {
            _logger = logger;
            _coatDomain = coatDomain;
        }

        [HttpGet]
        [Route("list")]
        public DTOResponse<List<DTOCoat>> GetAll()
        {
            return _coatDomain.GetAll();
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOCoat> Create(DTOCoat createCoatInfo)
        {
            return _coatDomain.Create(createCoatInfo);
        }

        [HttpPost]
        [Route("detail")]
        public DTOResponse<DTOCoat> GetById(DTOCoat getCoatByIdInfo)
        {
            return _coatDomain.GetById(getCoatByIdInfo.Id);
        }

        [HttpPost]
        [Route("update")]
        public DTOResponse<DTOCoat> Update(DTOCoat updateCoatInfo)
        {
            return _coatDomain.Update(updateCoatInfo);
        }

        [HttpPost]
        [Route("delete")]
        public DTOResponse<bool> Delete(DTOCoat deleteCoatByIdInfo)
        {
            return _coatDomain.DeleteById(deleteCoatByIdInfo.Id);
        }
    }
}
