using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Engagement;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EngagementController : IndexCrudController<GetLookUpDefinitionDTO, EngagementDTO, EngagementDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EngagementController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Engagement,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
