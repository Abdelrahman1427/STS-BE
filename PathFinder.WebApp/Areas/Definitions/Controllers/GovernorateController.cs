using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Governorate;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class GovernorateController : IndexCrudController<GetLookUpDefinitionDTO, GovernorateDTO, GovernorateDTO>
    {
        private readonly IHttpContextAccessor _context;
        public GovernorateController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Governorate,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

      
    }
}
