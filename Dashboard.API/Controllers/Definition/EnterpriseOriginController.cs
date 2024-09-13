using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.EntrepriseOrigin;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{

    [Route("EnterpriseOrigin")]
    [ApiController]
    public class EnterpriseOriginController : CrudWithPaginateController<EnterpriseOrigin, EnterpriseOriginDTO, EnterpriseOriginDTO, EnterpriseOriginDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEnterpriseOriginService _service;

        public EnterpriseOriginController(IMapper mapper, IEnterpriseOriginService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}