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
    [Route("furColor")]
    public class FurColorController : ControllerBase
    {
        private readonly ILogger<FurColorController> _logger;
        private readonly IFurColorDomain _furColorDomain;

        public FurColorController(ILogger<FurColorController> logger, IFurColorDomain furColorDomain)
        {
            _logger = logger;
            _furColorDomain = furColorDomain;
        }

        [HttpGet]
        [Route("list")]
        public DTOResponse<List<DTOFurColor>> GetAll()
        {
            return _furColorDomain.GetAll();
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOFurColor> Create(DTOFurColor createFurColorInfo)
        {
            return _furColorDomain.Create(createFurColorInfo);
        }

        [HttpPost]
        [Route("detail")]
        public DTOResponse<DTOFurColor> GetById(DTOFurColor getFurColorByIdInfo)
        {
            return _furColorDomain.GetById(getFurColorByIdInfo.Id);
        }
    }
}
