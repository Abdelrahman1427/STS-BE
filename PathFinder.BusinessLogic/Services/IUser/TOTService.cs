using Microsoft.AspNetCore.Http;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interfaces.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.BusinessLogic.Services.IUser
{
    public class TOTService : ITOTService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;
        public TOTService(IUnitOfWork unitOfWork, IHttpContextAccessor context) 
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
