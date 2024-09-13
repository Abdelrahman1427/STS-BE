using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.City;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Filters;

namespace STS.API.Controllers.Definition
{
    [Route("City")]
    [ApiController]
    public class CityController : CrudWithPaginateController<City, CityDTO, CityDTO, CityDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICityService _service;

        public CityController(IMapper mapper, ICityService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }

        [HttpGet("GetLookUp/{GovernorateId}")]
        [LoggerAction]
        public async Task<IActionResult> Get(int GovernorateId)
        {
            _predicate = c => c.GovernorateId == GovernorateId;
            var data = await _service.GetLookUpAsync(_predicate);
            var result = _mapper.Map<List<GetLookUpDefinitionDTO>>(data);
            return Ok(result);
        }
    }
}
