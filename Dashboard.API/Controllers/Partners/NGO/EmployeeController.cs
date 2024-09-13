using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IUserServices;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace STS.API.Controllers.Partners.NGO
{
    [Route("Employee")]
    [ApiController]
    public class EmployeeController : CrudWithPaginateController<Employee, EmployeeDTO, EmployeeDTO, EmployeeDTO, GetLookUpDefinitionDTO>
    {
        private IMapper _mapper;
        private IEmployeeService _service;

        public EmployeeController(IMapper mapper, IEmployeeService service, IHttpContextAccessor context) : base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
            _include = x => x.Include(a => a.CreatedByUser).Include(a => a.UpdatedByUser);
        }
    }
}
