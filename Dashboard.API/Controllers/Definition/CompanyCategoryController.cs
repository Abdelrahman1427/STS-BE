using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.DataTransferObjects.DTOs.CompanyCategory;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace Dashboard.API.Controllers.Definition
{
    [Route("CompanyCategory")]
    [ApiController]
    public class CompanyCategoryController : CrudWithPaginateController<CompanyCategory, CompanyCategoryDTO, CompanyCategoryDTO, CompanyCategoryDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private ICompanyCategoryService _service;

        public CompanyCategoryController(IMapper mapper, ICompanyCategoryService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}

