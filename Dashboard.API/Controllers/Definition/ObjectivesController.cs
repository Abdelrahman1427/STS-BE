using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Objectives;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    [Route("Objectives")]
    [ApiController]
    public class ObjectivesController : CrudWithPaginateController<Objective, ObjectivesDTO, ObjectivesDTO, ObjectivesDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IObjectivesService _service;

        public ObjectivesController(IMapper mapper, IObjectivesService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}