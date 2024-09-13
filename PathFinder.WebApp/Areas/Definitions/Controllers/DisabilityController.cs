using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Disability;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class DisabilityController : IndexCrudController<GetLookUpDefinitionDTO, DisabilityDTO, DisabilityDTO>
    {
        private readonly IHttpContextAccessor _context;
        public DisabilityController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Disability,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

      

    }
}
