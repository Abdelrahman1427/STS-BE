using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.EntrepriseType;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EnterpriseTypeController : IndexCrudController<GetLookUpDefinitionDTO, EnterpriseTypeDTO, EnterpriseTypeDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EnterpriseTypeController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.EnterpriseType,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       
    }
}
