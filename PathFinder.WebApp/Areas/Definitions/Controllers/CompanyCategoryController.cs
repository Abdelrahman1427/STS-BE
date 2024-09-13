using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.CompanyCategory;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;


using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class CompanyCategoryController : IndexCrudController<GetLookUpDefinitionDTO, CompanyCategoryDTO, CompanyCategoryDTO>
    {
        private readonly IHttpContextAccessor _context;
        public CompanyCategoryController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.CompanyCategory,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

       

    }
}
