﻿using Microsoft.AspNetCore.Http;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interfaces.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.BusinessLogic.Services.IUser
{
    public class BeneficiariesService : CrudWithPaginateService<Beneficiarie>, IBeneficiariesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;
        public BeneficiariesService(IUnitOfWork unitOfWork, IHttpContextAccessor context)  :base(unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
