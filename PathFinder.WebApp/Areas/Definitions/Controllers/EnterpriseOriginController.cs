using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.EntrepriseOrigin;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class EnterpriseOriginController : IndexCrudController<GetLookUpDefinitionDTO, EnterpriseOriginDTO, EnterpriseOriginDTO>
    {
        private readonly IHttpContextAccessor _context;
        public EnterpriseOriginController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.EnterpriseOrigin,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
