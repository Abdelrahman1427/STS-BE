using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.CompanySector;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class CompanySectorController : IndexCrudController<GetLookUpDefinitionDTO, CompanySectorDTO, CompanySectorDTO>
    {
        private readonly IHttpContextAccessor _context;
        public CompanySectorController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.CompanySector,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }



    }
}
