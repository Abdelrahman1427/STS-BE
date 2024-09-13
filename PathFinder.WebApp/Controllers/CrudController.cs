using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Helpers;
using Newtonsoft.Json;

namespace PathFinder.WebApp.Controllers
{
    public class CrudController<AddDTO, UpdateDTO> : Controller
        where AddDTO : class, new()
        where UpdateDTO : class
    {
        protected IHttpRequestClient _httpClient;
        protected readonly string _getByIdURL;
        protected readonly string _addURL;
        protected readonly string _updateURL;
        protected readonly string _deleteURL;
        protected readonly string _addPartialView;
        protected readonly string _updatePartialView;
        public CrudController(IHttpRequestClient httpClient, string getByIdURL, string addURL, string updateURL, string addPartialView, string updatePartialView, string deleteURL = null)
        {
            _httpClient = httpClient;
            _getByIdURL = getByIdURL;
            _addURL = addURL;
            _updateURL = updateURL;
            _deleteURL = deleteURL;
            _addPartialView = addPartialView;
            _updatePartialView = updatePartialView;
        }
        public CrudController(IHttpRequestClient httpClient, string baseURL, string addPartialView, string updatePartialView)
        {
            _httpClient = httpClient;
            _getByIdURL = $"{baseURL}/getbyid/";
            _addURL = $"{baseURL}/add";
            _updateURL = $"{baseURL}/update/";
            _deleteURL = $"{baseURL}/delete/";
            _addPartialView = addPartialView;
            _updatePartialView = updatePartialView;
        }

        [HttpGet]
        //[DynamicAuthorize]
        public virtual async Task<IActionResult> Add()
        {
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            return PartialView(_addPartialView, new AddDTO());
        }
        // POST: Controller/Add
        [HttpPost]
        //[DynamicAuthorize]
        public virtual async Task<IActionResult> Add(AddDTO model)
        {
            if (!ModelState.IsValid)
                return await new JsonResponse().GetJsonResultAsync(this, _addPartialView, model);

            var response = await _httpClient.PostAsync<AddDTO>(_addURL, model );
            if (!response.IsSuccess) await response.SetHtmlToPartialView(this, _addPartialView, model, response.Message);
            else if(!response.Value.state) await response.SetHtmlToPartialView(this, _addPartialView, model, response.Value.message);
            else response.Message = CoreResources.AddSuccessMessage;
            return Json(response);
        }

        [HttpPost]
        //[DynamicAuthorize]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.DeleteAsync<APIResult>($"{_deleteURL}{id}" );
            response.Message = response.IsSuccess ? response.Value.state ? CoreResources.DeleteSuccessMessage : response.Value.message : response.Message;
            return Json(response);
        }

        [HttpGet]
        //[DynamicAuthorize]
        public virtual async Task<IActionResult> Update(string id)
        {
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            var result = await _httpClient.GetAsync<APIResult>($"{_getByIdURL}{id}" );
            UpdateDTO updateDTO = JsonConvert.DeserializeObject<UpdateDTO>(result.Value.entity?.ToString())!;
            return PartialView(_updatePartialView, updateDTO);
        }

        [HttpPost]
        //[DynamicAuthorize]
        public virtual async Task<IActionResult> Update(string id,UpdateDTO model)
        {
            if (!ModelState.IsValid)
                return await new JsonResponse().GetJsonResultAsync(this, _updatePartialView, model);

            var response = await _httpClient.PutAsync<UpdateDTO>($"{_updateURL}{id}", model );
            if (!response.IsSuccess) await response.SetHtmlToPartialView(this, _updatePartialView, model, response.Message);
            else response.Message = CoreResources.UpdateSuccessMessage;
            return Json(response);
        }
    }
}
