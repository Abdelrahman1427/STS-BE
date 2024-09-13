using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Role;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class RoleController : IndexCrudController<GetLookUpDefinitionDTO, RoleDTO, RoleDTO>
    {
        private readonly IHttpContextAccessor _context;
        public RoleController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Role,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
