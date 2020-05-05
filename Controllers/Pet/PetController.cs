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
    [Route("pet")]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly IPetDomain _petDomain;

        public PetController(ILogger<PetController> logger, IPetDomain petDomain)
        {
            _logger = logger;
            _petDomain = petDomain;
        }

        [HttpGet]
        [Route("list")]
        public DTOResponse<List<DTOPet>> GetAllPets()
        {
            return _petDomain.GetAllPets();
        }

        [HttpPost]
        [Route("fromUser")]
        public DTOResponse<List<DTOPet>> GetAllPetsFromUser(DTOInfo loginInfo)
        {
            return _petDomain.GetPetsByUserId(loginInfo.Id);
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOPet> Create(DTOPet createPetInfo)
        {
            return _petDomain.Create(createPetInfo);
        }
    }
}
