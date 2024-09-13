using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.DataTransferObjects.DTOs.NGO;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Filters;

namespace Dashboard.API.Controllers.Partners.NGO
{
    [Route("NGO")]
    [ApiController]
    public class NGOController : CrudWithPaginateController<NonGovermntalOrgniszation, NgoDTO, NgoDTO, NgoDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private INGOService _service;

        public NGOController(IMapper mapper, INGOService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            //_include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }

        [HttpGet("GetLookUp")]
        [LoggerAction]
        public override async Task<IActionResult> GetLookUp()
        {
            var data = await _service.GetLookUpAsync(_predicate);
            var result = _mapper.Map<List<GetLookUpNgoDTO>>(data);
            return Ok(result);
        }
    }
}
