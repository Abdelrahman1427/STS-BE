using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Partner;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class PartnerController : IndexCrudController<GetLookUpDefinitionDTO, PartnerDTO, PartnerDTO>
    {
        private readonly IHttpContextAccessor _context;
        public PartnerController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Partner,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

        

    }
}
