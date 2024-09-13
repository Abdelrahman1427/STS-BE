using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.DataTransferObjects.DTOs.Product;

namespace STS.API.Controllers.Definition
{
    [Route("Product")]
    [ApiController]
    public class ProductController : CrudWithPaginateController<Product, AddUpdateProductDTO, AddUpdateProductDTO, AddUpdateProductDTO, GetPageProductDTO>
    {
        private IMapper _mapper;
        private IProductService _service;

        public ProductController(IMapper mapper, IProductService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}
