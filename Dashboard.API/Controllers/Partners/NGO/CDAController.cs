using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.CDA;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.DataTransferObjects.DTOs.NGO;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Filters;

namespace STS.API.Controllers.Partners.NGO
{
    [Route("CDA")]
    [ApiController]
    public class CDAController : CrudWithPaginateController<CommunityDevlopmentAssosition, CDAdto, CDAdto, CDAdto, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICDAService _service;

        public CDAController(IMapper mapper, ICDAService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
           // _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }

    }
}
