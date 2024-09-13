using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.EngagementModality;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
namespace Dashboard.API.Controllers.Definition
{

    [Route("EngagementModality")]
    [ApiController]
    public class EngagementModalityController : CrudWithPaginateController<EngagementModality, EngagementModalityDTO, EngagementModalityDTO, EngagementModalityDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEngagementModalityService _service;

        public EngagementModalityController(IMapper mapper, IEngagementModalityService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}