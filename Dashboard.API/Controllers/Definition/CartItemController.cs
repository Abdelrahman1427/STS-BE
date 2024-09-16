using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.DataTransferObjects.DTOs.CartItem;
using STS.DataTransferObjects.Helpers;


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
            _include = x => x.Include(a => a.Product);
        }

        [HttpGet("GetCartTotalPrice")]
        public async Task<IActionResult> GetCartTotalPrice()
        {
            try
            {
                var total = await _service.GetTotalPrice();
                return Ok(total);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateQuantity/{id}")]
        public virtual async Task<APIResult> UpdateQuantity(int id, QuantityUpdateDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new APIResult { state = false, entity = ModelState };
                var entity = await _service.GetByIdAsync(id);
                _mapper.Map(request, entity);
                await _service.UpdateAsync(entity);
                return new APIResult { state = true, entity = entity };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

    }
}
