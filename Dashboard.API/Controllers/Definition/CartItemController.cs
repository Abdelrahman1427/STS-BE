using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.DataTransferObjects.DTOs.CartItem;


namespace STS.API.Controllers.Definition
{
    [Route("CartItem")]
    [ApiController]
    public class CartItemController : CrudWithPaginateController<CartItem, AddUpdateCartItemDTO, AddUpdateCartItemDTO, AddUpdateCartItemDTO, GetPageCartItemDTO>
    {
        private IMapper _mapper;
        private ICartItemService _service;

        public CartItemController(IMapper mapper, ICartItemService service) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }
    }
}
