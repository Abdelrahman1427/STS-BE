using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.EngagementModality;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EngagementModalityController : IndexCrudController<GetLookUpDefinitionDTO, EngagementModalityDTO, EngagementModalityDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EngagementModalityController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.EngagementModality,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }


    }
}
