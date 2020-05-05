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
    [Route("partnersCategory")]
    public class PartnerCategoryController : ControllerBase
    {
        private readonly ILogger<PartnerCategoryController> _logger;
        private readonly IPartnerCategoryDomain _domain;

        public PartnerCategoryController(ILogger<PartnerCategoryController> logger, IPartnerCategoryDomain domain)
        {
            _logger = logger;
            _domain = domain;
        }

        [HttpGet]
        [Route("list")]
        public DTOResponse<List<DTOPartnerCategory>> GetAll()
        {
            return _domain.GetAll();
        }

        [HttpPost]
        [Route("create")]
        public DTOResponse<DTOPartnerCategory> Create(DTOPartnerCategory createInfo)
        {
            return _domain.Create(createInfo);
        }

        [HttpPost]
        [Route("detail")]
        public DTOResponse<DTOPartnerCategory> GetById(DTOPartnerCategory getByIdInfo)
        {
            return _domain.GetById(getByIdInfo.Id);
        }

        [HttpPost]
        [Route("update")]
        public DTOResponse<DTOPartnerCategory> Update(DTOPartnerCategory updateInfo)
        {
            return _domain.Update(updateInfo);
        }

        [HttpPost]
        [Route("delete")]
        public DTOResponse<bool> Delete(DTOPartnerCategory deleteByIdInfo)
        {
            return _domain.DeleteById(deleteByIdInfo.Id);
        }
    }
}
