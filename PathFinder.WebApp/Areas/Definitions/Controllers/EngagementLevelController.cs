using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.EngagementLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EngagementLevelController : IndexCrudController<GetLookUpDefinitionDTO, EngagementLevelDTO, EngagementLevelDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EngagementLevelController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.EngagementLevel,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

      

    }
}
