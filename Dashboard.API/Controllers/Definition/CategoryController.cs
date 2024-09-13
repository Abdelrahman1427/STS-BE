using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.DataTransferObjects.DTOs.Category;

namespace STS.API.Controllers.Definition
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : CrudWithPaginateController<Category, AddUpdateCategoryDTO, AddUpdateCategoryDTO, AddUpdateCategoryDTO, GetPageCategoryDTO>
    {
        private IMapper _mapper;
        private ICategoryService _service;

        public CategoryController(IMapper mapper, ICategoryService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}
