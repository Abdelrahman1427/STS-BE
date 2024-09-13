using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.Engagement;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    
    [Route("Engagement")]
    [ApiController]
    public class EngagementController : CrudWithPaginateController<Engagement, EngagementDTO, EngagementDTO, EngagementDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEngagementService _service;

        public EngagementController(IMapper mapper, IEngagementService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}