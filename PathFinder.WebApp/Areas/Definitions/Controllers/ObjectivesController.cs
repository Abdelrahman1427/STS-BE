using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Objectives;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class ObjectivesController : IndexCrudController<GetLookUpDefinitionDTO, ObjectivesDTO, ObjectivesDTO>
    {
        private readonly IHttpContextAccessor _context;
        public ObjectivesController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Objectives,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }
    }
}