using Autofac.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PathFinder.BusinessLogic.Services.IUser;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.DataTransferObjects.DTOs.BeneficiariesCourse;
using PathFinder.DataTransferObjects.DTOs.City;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System.Net.Http;

namespace STS.API.Controllers.DataEntry
{
    [Route("CourseTransaction")]
    [ApiController]
    public class CourseTransactionController : CrudWithPaginateController<CourseTransaction, CourseTransactionDTO, CourseTransactionDTO, CourseTransactionDTO, GetPageBeneficiariesCourseDTO>
    {
        private readonly ICourseTransactionService _courseTransactionService;
        //private readonly IHttpContextAccessor _httpContext;
        private IMapper _mapper;

        public CourseTransactionController(IMapper mapper, ICourseTransactionService courseTransactionService) : base(mapper, courseTransactionService)
        {
            _mapper = mapper;
            _courseTransactionService = courseTransactionService;
            //_httpContext = httpContext;
        }

        //[HttpPost("Add")]
        //public async Task<IActionResult> AddBeneficiarie(CourseTransactionDTO model)
        //{
        //    var company = _mapper.Map<CourseTransaction>(model);
        //    await _courseTransactionService.SeedCompanyFeatureOnUpdate(company);


        //    return Ok(model);
        //}


    }
}
