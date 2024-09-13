using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Governorate;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

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
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}
