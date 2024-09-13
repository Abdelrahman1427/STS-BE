using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.CompanyStatus;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
namespace STS.API.Controllers.Definition
{

    [Route("CompanyStatus")]
    [ApiController]
    public class CompanyStatusController : CrudWithPaginateController<CompanyStatus, CompanyStatusDTO, CompanyStatusDTO, CompanyStatusDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICompanyStatusService _service;

        public CompanyStatusController(IMapper mapper, ICompanyStatusService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}