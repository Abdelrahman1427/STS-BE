using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Disability;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using Microsoft.EntityFrameworkCore;

namespace STS.API.Controllers.Definition
{
    [Route("Disability")]
    [ApiController]
    public class DisabilityController : CrudWithPaginateController<Disability, DisabilityDTO, DisabilityDTO, DisabilityDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IDisabilityService _service;

        public DisabilityController(IMapper mapper, IDisabilityService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}