using Microsoft.AspNetCore.Http;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Entities;
using PathFinder.Core.Interfaces.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinder.Core.Interface.IClientServices;
using System.Threading.Tasks;

namespace PathFinder.BusinessLogic.Services.IClient
{
    public class CompanyCategoryService : CrudWithPaginateService<CompanyCategory>, ICompanyCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public CompanyCategoryService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
