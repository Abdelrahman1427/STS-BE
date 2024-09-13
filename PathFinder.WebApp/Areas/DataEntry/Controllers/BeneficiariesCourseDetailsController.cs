using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.BeneficiariesCourse;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;
using PathFinder.DataTransferObjects.DTOs.Interventiom;
using PathFinder.DataTransferObjects.DTOs.NGO;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.WebApp.Areas.Definitions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.DataEntry.Controllers
{
    [Authorize]
    [Area("DataEntry")]
    public class BeneficiariesCourseDetailsController : IndexCrudController<GetPageBeneficiariesCourseDTO, BeneficiariesCourseDTO, BeneficiariesCourseDTO>
    {
        protected IHttpRequestClient _httpClient;

        public BeneficiariesCourseDetailsController(IHttpRequestClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, DataEntryURLRoutes.CourseTransaction.ControllerName,
      "_AddUpdateForm", "_AddUpdateForm")
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        public override async Task<IActionResult> Add()
        {
            await SetViewBag();
            return PartialView("_AddUpdateForm", new BeneficiariesCourseDTO());
        }


        public override async Task<IActionResult> Add(BeneficiariesCourseDTO model)
        {
            if (!ModelState.IsValid )
            {
                await SetViewBag();
                return View("_AddUpdateForm", model);

            }
            return await base.Add(model);
        }
        public override async Task<IActionResult> Update(string id)
        {
            await SetViewBag();
            return await base.Update(id);
        }

        public override async Task<IActionResult> Update(string id, BeneficiariesCourseDTO model)
        {
            if (!ModelState.IsValid)
            {
                await SetViewBag();
                return View("_AddUpdateForm", model);
            }
            return await base.Update(id, model);
        }

        private async Task SetViewBag()
        {
            var Governorates = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpGovernorate);
            ViewBag.Governorate = Governorates.Value;

            var objective = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpObjectives);
            ViewBag.Objective = objective.Value;

            var intervention = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpIntervention);
            ViewBag.Intervention = intervention.Value;

            var CDA = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpCDA);
            ViewBag.CDA = CDA.Value;

            var partner = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpPartner);
            ViewBag.Partner = partner.Value;

            var NGO = await _httpClient.GetAsync<List<GetLookUpNgoDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpNGO);
            ViewBag.NGO = NGO.Value;

            var educationLevel = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpEducationLevel);
            ViewBag.EducationLevel = educationLevel.Value;

            var disability = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpDisability);
            ViewBag.Disability = educationLevel.Value;


        }

    }
}
