using Microsoft.AspNetCore.Http;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.Core.Interfaces.Shared.Repository;

namespace STS.BusinessLogic.Services.DefinitionServices
{
    public class CategoryService : CrudWithPaginateService<Category>, ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public CategoryService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
