using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.CompanyStatus;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class CompanyStatusController : IndexCrudController<GetLookUpDefinitionDTO, CompanyStatusDTO, CompanyStatusDTO>
    {
        private readonly IHttpContextAccessor _context;
        public CompanyStatusController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.CompanyStatus,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
