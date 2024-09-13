using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.City;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Extensions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.Definitions.Controllers
{
    [Authorize]
    [Area("Definitions")]
    public class CityController : IndexCrudController<GetLookUpDefinitionDTO, CityDTO, CityDTO>
    {
        private readonly IHttpContextAccessor _context;
        public CityController(IHttpRequestClient httpClient, IHttpContextAccessor context) : base(httpClient, DefinitionsURLRoutes.Definitions.City,
            "_AddUpdateForm", "_AddUpdateForm")
        {
            _context = context;
        }
        public override async Task<IActionResult> Add()
        {
            ViewBag.Governorate = await GetGovernorateAsync();
            return await base.Add();
        }

        public override async Task<IActionResult> Add(CityDTO model)
        {
            string? userId = _context.GetUserName();
            if (!ModelState.IsValid || userId == null)
            {
                ViewBag.Governorate = await GetGovernorateAsync();
                return View("_AddUpdateForm", model);

            }
            model.CreatedBy = userId.ToString();
            model.CreatedDate = DateTime.Now;
            return await base.Add(model);
        }

        public override async Task<IActionResult> Update(string id)
        {
            ViewBag.Governorate = await GetGovernorateAsync();
            return await base.Update(id);
        }

        public override async Task<IActionResult> Update(string id, CityDTO model)
        {
            if (!ModelState.IsValid )
            {
                ViewBag.Governorate = await GetGovernorateAsync();
                return View( "_AddUpdateForm", model);
            }
            return await base.Update(id, model);
        }

        async Task<List<GetLookUpDefinitionDTO>> GetGovernorateAsync()
        {
            var Governorates = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpGovernorate);
            return Governorates.Value;
        }
    }
}
