using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.Interventiom;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class InterventionController : IndexCrudController<GetLookUpDefinitionDTO, InterventionDTO, InterventionDTO>
    {
        private readonly IHttpContextAccessor _context;
        public InterventionController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.Intervention,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }

        public override async Task<IActionResult> Add()
        {
            ViewBag.Objectives = await GetObjectiveAsync();
            return await base.Add();
        }

        public override async Task<IActionResult> Add(InterventionDTO model)
        {
            string? userId = _context.GetUserName();
            if (!ModelState.IsValid || userId == null)
            {
                ViewBag.Objectives = await GetObjectiveAsync();
                return View("_AddUpdateForm", model);

            }
            model.CreatedBy = userId.ToString();
            model.CreatedDate = DateTime.Now;
            return await base.Add(model);
        }

        public override async Task<IActionResult> Update(string id)
        {
            ViewBag.Objectives = await GetObjectiveAsync();
            return await base.Update(id);
        }

        public override async Task<IActionResult> Update(string id, InterventionDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Objectives = await GetObjectiveAsync();
                return View("_AddUpdateForm", model);
            }
            return await base.Update(id, model);
        }
        async Task<List<GetLookUpDefinitionDTO>> GetObjectiveAsync()
        {
            var Objectives = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpObjective);
            return Objectives.Value;
        }
    }
}
