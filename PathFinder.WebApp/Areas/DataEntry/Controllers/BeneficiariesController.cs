using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.DTOs.CourseTransaction;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.WebApp.Areas.Definitions;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Controllers;

namespace PathFinder.WebApp.Areas.DataEntry.Controllers
{
    [Authorize]
    [Area("DataEntry")]
    public class BeneficiariesController : IndexCrudController<GetPageBeneficiarieDTO, BeneficiarieDTO, BeneficiarieDTO>
    {
        protected IHttpRequestClient _httpClient;

        public BeneficiariesController(IHttpRequestClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, DataEntryURLRoutes.Beneficiaries.ControllerName,
              "_AddUpdateForm", "_AddUpdateForm")
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public override async Task<IActionResult>  Add()
        {
            await SetViewBag();
            return PartialView("_AddUpdateForm", new BeneficiarieDTO());
        }


        [HttpGet]
        public async Task<IActionResult> CourseTransaction()
        {
            await SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourseTransaction(CourseTransactionDTO model)
        {
            string message;
            if (!ModelState.IsValid)
            {
                await SetViewBag();
                return View();
            }
            var result = await _httpClient.PostAsync(DataEntryURLRoutes.CourseTransaction.AddCourseTransaction, model);

            if (!result.IsSuccess)
            {
                message = result.Message;
                return View();
            }
            else if (!result.Value.state)
            {
                message = result.Value.message;
                return View();
            }
            else
                message = ModelValidationResources.SuccessMessage;

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<JsonResult> GetCity(int GovernorateId)
        {
            var city = (await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>($"{CoreURLRoutes.City.GetLookUp}{GovernorateId}")).Value;
            return Json(city);
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

            var NGO = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpEmployee);
            ViewBag.NGO = NGO.Value;

            var educationLevel = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpEducationLevel);
            ViewBag.EducationLevel = educationLevel.Value;

            var disability = await _httpClient.GetAsync<List<GetLookUpDefinitionDTO>>(DefinitionsURLRoutes.LookUp.GetLookUpDisability);
            ViewBag.Disability = educationLevel.Value;


        }
    }
}
