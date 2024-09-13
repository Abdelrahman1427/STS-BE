using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.EntrepriseType;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    [Route("EnterpriseType")]
    [ApiController]
    public class EnterpriseTypeController : CrudWithPaginateController<EnterpriseType, EnterpriseTypeDTO, EnterpriseTypeDTO, EnterpriseTypeDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEnterpriseTypeService _service;

        public EnterpriseTypeController(IMapper mapper, IEnterpriseTypeService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}