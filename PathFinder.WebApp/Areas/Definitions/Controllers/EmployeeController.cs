using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EmployeeController : IndexCrudController<GetLookUpDefinitionDTO, EmployeeDTO, EmployeeDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EmployeeController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Employee,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
