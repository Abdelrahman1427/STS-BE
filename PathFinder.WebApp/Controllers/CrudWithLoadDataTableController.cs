using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Models;
using PathFinder.WebApp.Clients;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.WebApp.Controllers
{
    public class CrudWithLoadDataTableController<GetDTO, AddDTO, UpdateDTO> : Controller
        where GetDTO : class, new()
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
        protected readonly string _getPageURL;
        public CrudWithLoadDataTableController(IHttpRequestClient httpClient, string getByIdURL, string addURL, string updateURL, string addPartialView, string updatePartialView, string getPageURL = null, string deleteURL = null)
        {
            _httpClient = httpClient;
            _getByIdURL = getByIdURL;
            _addURL = addURL;
            _updateURL = updateURL;
            _deleteURL = deleteURL;
            _addPartialView = addPartialView;
            _updatePartialView = updatePartialView;
            _getPageURL = getPageURL;
        }
        public CrudWithLoadDataTableController(IHttpRequestClient httpClient, string baseURL, string addPartialView, string updatePartialView)
        {
            _httpClient = httpClient;
            _getByIdURL = $"{baseURL}/getbyid/";
            _addURL = $"{baseURL}/add";
            _updateURL = $"{baseURL}/update/";
            _deleteURL = $"{baseURL}/delete/";
            _getPageURL = $"{baseURL}/getpage";
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

        [HttpPost]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.DeleteAsync<int>($"{_deleteURL}{id}" );
            response.Message = response.IsSuccess ? CoreResources.DeleteSuccessMessage : response.Message;
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
            return Json(data.GetDataTableModel(result.Value.entity));
        }
    }
}
