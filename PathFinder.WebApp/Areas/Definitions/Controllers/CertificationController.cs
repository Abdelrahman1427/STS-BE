using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Certification;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class CertificationController : IndexCrudController<GetLookUpDefinitionDTO, CertificationDTO, CertificationDTO>
    {
        public CertificationController(IHttpRequestClient httpClient) : base(httpClient, DefinitionsURLRoutes.Definitions.Certification,
            "_AddUpdateForm", "_AddUpdateForm")
        {
        }
    }
}