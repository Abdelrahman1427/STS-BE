using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STS.Core.Interface.IClientServices;
using STS.DataTransferObjects.DTOs.Governorate;
using STS.DataTransferObjects.DTOs.Shared.Request;

namespace STS.API.Controllers.Definition
{
    [Route("Governorate")]
    [ApiController]
    public class GovernorateController : CrudWithPaginateController<Governorate, GovernorateDTO, GovernorateDTO, GovernorateDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IGovernorateService _service;

        public GovernorateController(IMapper mapper, IGovernorateService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}
