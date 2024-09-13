using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.UserType;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;





using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class UserTypeController : IndexCrudController<GetLookUpDefinitionDTO, UserTypeDTO, UserTypeDTO>
    {
        private readonly IHttpContextAccessor _context;
        public UserTypeController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.UserType,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       
    }
}
