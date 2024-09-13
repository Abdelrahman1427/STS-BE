using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.CompanyCategory;
using PathFinder.DataTransferObjects.DTOs.CompanySector;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
namespace Dashboard.API.Controllers.Definition
{

    [Route("CompanySector")]
    [ApiController]
    public class CompanySectorController : CrudWithPaginateController<CompanySector, CompanySectorDTO, CompanySectorDTO, CompanyCategoryDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICompanySectorService _service;

        public CompanySectorController(IMapper mapper, ICompanySectorService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}

