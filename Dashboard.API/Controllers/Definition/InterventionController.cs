using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Interventiom;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Filters;

namespace STS.API.Controllers.Definition
{
    [Route("Intervention")]
    [ApiController]
    public class InterventionController : CrudWithPaginateController<Intervention, InterventionDTO, InterventionDTO, InterventionDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IInterventionService _service;

        public InterventionController(IMapper mapper, IInterventionService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }

        [HttpGet("GetLookUp/{ObjectiveId}")]
        [LoggerAction]
        public async Task<IActionResult> Get(int ObjectiveId)
        {
            _predicate = c => c.ObjectiveId == ObjectiveId;
            var data = await _service.GetLookUpAsync(_predicate);
            var result = _mapper.Map<List<GetLookUpDefinitionDTO>>(data);
            return Ok(result);
        }
    }
}
