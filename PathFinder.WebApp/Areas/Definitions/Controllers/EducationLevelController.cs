using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.EducationLevel;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EducationLevelController : IndexCrudController<GetLookUpDefinitionDTO, EducationLevelDTO, EducationLevelDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EducationLevelController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.EducationLevel,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

        
    }
}
