using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.EngagementLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace STS.API.Controllers.Definition
{
    [Route("EngagementLevel")]
    [ApiController]
    public class EngagementLevelController : CrudWithPaginateController<EngagementLevel, EngagementLevelDTO, EngagementLevelDTO, EngagementLevelDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEngagementLevelService _service;

        public EngagementLevelController(IMapper mapper, IEngagementLevelService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}