﻿using Microsoft.AspNetCore.Http;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Entities;
using PathFinder.Core.Interfaces.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Core.Interface.IClientServices;

namespace PathFinder.BusinessLogic.Services.IClient
{
    public class AssessmentService : CrudWithPaginateService<Assessment>, IAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public AssessmentService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
    }
}
