using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.Partner;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace STS.API.Controllers.Definition
{
    [Route("Partner")]
    [ApiController]
    public class PartnerController : CrudWithPaginateController<Partner, PartnerDTO, PartnerDTO, PartnerDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IPartnerService _service;

        public PartnerController(IMapper mapper, IPartnerService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}
