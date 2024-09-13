using Microsoft.AspNetCore.Http;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Interfaces.Shared.Repository;
using STS.Core.Interface.IDefinitionServices;
using STS.Core.Entities;
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
    }
}

