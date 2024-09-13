using Microsoft.AspNetCore.Http;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Entities;
using PathFinder.Core.Interfaces.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Core.Interface.IUserServices;

namespace PathFinder.BusinessLogic.Services.IUser
{
    public class EngagementService : CrudWithPaginateService<Engagement>, IEngagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public EngagementService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
