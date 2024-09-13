using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.WebApp.Controllers
{
    public class AddUpdateController<GetDTO,AddDTO, UpdateDTO> : Controller
        where GetDTO : class, new()
        where AddDTO : class, new() 
        where UpdateDTO : class
    {
        protected IHttpRequestClient _httpClient;
        protected readonly string _getPageURL;
        protected readonly string _getByIdURL;
        protected readonly string _addURL;
        protected readonly string _updateURL;
        protected readonly string _addPartialView;
        protected readonly string _updatePartialView;
        public AddUpdateController(IHttpRequestClient httpClient, string baseURL, string addPartialView, string updatePartialView)
        {
            _httpClient = httpClient;
            _getPageURL = $"{baseURL}/getpage";
            _getByIdURL = $"{baseURL}/getbyid/"; 
            _addURL = $"{baseURL}/add";
            _updateURL = $"{baseURL}/update/";
            _addPartialView = addPartialView;
            _updatePartialView = updatePartialView;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Add()
        {
            return PartialView(_addPartialView, new AddDTO());
        }
        // POST: Controller/Add
        [HttpPost]
        public virtual async Task<IActionResult> Add(AddDTO model)
        {
            if (!ModelState.IsValid)
                return await new JsonResponse().GetJsonResultAsync(this, _addPartialView, model);

            var response = await _httpClient.PostAsync<AddDTO>(_addURL, model );
            if (!response.IsSuccess) await response.SetHtmlToPartialView(this, _addPartialView, model, response.Message);
            else response.Message = CoreResources.AddSuccessMessage;
            return Json(response);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Update(string id)
        {
            var result = await _httpClient.GetAsync<UpdateDTO>($"{_getByIdURL}{id}" );
            return PartialView(_updatePartialView, result.Value);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(string id, UpdateDTO model)
        {
            if (!ModelState.IsValid)
                return await new JsonResponse().GetJsonResultAsync(this, _updatePartialView, model);

            var response = await _httpClient.PutAsync<UpdateDTO>($"{_updateURL}{id}", model );
            if (!response.IsSuccess) await response.SetHtmlToPartialView(this, _updatePartialView, model, response.Message);
            else response.Message = CoreResources.UpdateSuccessMessage;
            return Json(response);
        }

        [HttpPost]
        public virtual async Task<JsonResult> LoadDataTable(DataTableModel data)
        {
            var page = new PagingDTO()
            {
                //IsOrderAsc = data.Order[0].Dir == "asc" ? true : false,
                //OrderProp = data.Columns[data.Order[0].Column].Name,
                //Page = data.PageNo,
                PageSize = data.Length,
            };
            var result = await _httpClient.PostAsync<Pagination<GetDTO>, PagingDTO>(_getPageURL, page );
            //return Json(data.GetDataTableModel(result.Value));
            return Json(data.GetDataTableModel(result.Value.entity));
        }
    }
}