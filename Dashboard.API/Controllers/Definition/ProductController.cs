using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.DataTransferObjects.DTOs.Product;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.DataTransferObjects.Helpers;
using System.Linq.Expressions;

namespace STS.API.Controllers.Definition
{
    [Route("Product")]
    [ApiController]
    public class ProductController : CrudController<Product, AddUpdateProductDTO, AddUpdateProductDTO, AddUpdateProductDTO, GetPageProductDTO>
    {
        private IMapper _mapper;
        private IProductService _service;
        protected Expression<Func<Product, bool>> _predicate;
        protected Func<IQueryable<Product>, IIncludableQueryable<Product, object>> _include;
        public ProductController(IMapper mapper, IProductService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.Category);

        }
        [HttpPost("GetPage")]
        public virtual async Task<APIResult> GetPage(PagingDTO<FilterProductDTO> paginationDTO)
        {
            try
            {
                var data = await _service.GetPageAsync( paginationDTO);
                var result = _mapper.Map<Pagination<GetPageProductDTO>>(data);
                return new APIResult { state = true, entity = result };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpGet("GetLookUp")]
        public virtual async Task<IActionResult> GetLookUp()
        {
            var data = await _service.GetLookUpAsync(_predicate, _include);
            var result = _mapper.Map<List<GetPageProductDTO>>(data);
            return Ok(result);
        }
    }
}
