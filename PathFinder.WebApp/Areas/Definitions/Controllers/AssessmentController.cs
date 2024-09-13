using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Assessment;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class AssessmentController : IndexCrudController<GetLookUpDefinitionDTO, AssessmentDTO, AssessmentDTO>
    {
        public AssessmentController(IHttpRequestClient httpClient) : base(httpClient, DefinitionsURLRoutes.Definitions.Assessment,
            "_AddUpdateForm", "_AddUpdateForm")
        {
        }
    }
}