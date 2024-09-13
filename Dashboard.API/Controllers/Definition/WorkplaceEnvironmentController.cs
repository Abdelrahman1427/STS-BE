using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.DTOs.WorkplaceEnvironment;

namespace STS.API.Controllers.Definition
{
    [Route("WorkplaceEnvironment")]
    [ApiController]
    public class WorkplaceEnvironmentController : CrudWithPaginateController<WorkplaceEnvironment, WorkplaceEnvironmentDTO, WorkplaceEnvironmentDTO, WorkplaceEnvironmentDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IWorkplaceEnvironmentService _service;

        public WorkplaceEnvironmentController(IMapper mapper, IWorkplaceEnvironmentService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}