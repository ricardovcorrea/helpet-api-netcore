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
    [Route("breed")]
    public class BreedController : ControllerBase
    {
        private readonly ILogger<BreedController> _logger;
        private readonly IBreedDomain _breedDomain;

        public BreedController(ILogger<BreedController> logger, IBreedDomain breedDomain)
        {
            _logger = logger;
            _breedDomain = breedDomain;
        }

        [HttpGet]
        [Route("list")]
        public DTOResponse<List<DTOBreed>> GetAll()
        {
            return _breedDomain.GetAll();
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOBreed> Create(DTOBreed createBreedInfo)
        {
            return _breedDomain.Create(createBreedInfo);
        }

        [HttpPost]
        [Route("detail")]
        public DTOResponse<DTOBreed> GetById(DTOBreed getBreedByIdInfo)
        {
            return _breedDomain.GetById(getBreedByIdInfo.Id);
        }

        [HttpPost]
        [Route("update")]
        public DTOResponse<DTOBreed> Update(DTOBreed updateBreedInfo)
        {
            return _breedDomain.Update(updateBreedInfo);
        }

        [HttpPost]
        [Route("delete")]
        public DTOResponse<bool> Delete(DTOBreed deleteBreedByIdInfo)
        {
            return _breedDomain.DeleteById(deleteBreedByIdInfo.Id);
        }
    }
}
