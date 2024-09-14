using Microsoft.AspNetCore.Http;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Interfaces.Shared.Repository;
using STS.Core.Interface.IDefinitionServices;
using STS.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace STS.BusinessLogic.Services.IClient
{
    public class CartItemService : CrudWithPaginateService<CartItem>, ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public CartItemService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<decimal> GetTotalPrice()
        {
            try
            {
                var userList = await _unitOfWork.GetRepositoryAsync<CartItem>()
                    .GetListAsync(include: src => src.Include(x => x.Product));

                var totalPrice = userList.Sum(c => c.Quantity * c.Product.Price);

                return totalPrice;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

