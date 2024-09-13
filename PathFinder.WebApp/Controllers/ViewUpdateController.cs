using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.WebApp.Controllers
{
    public class ViewUpdateController<GetDTO,ViewDTO, UpdateDTO> : Controller
        where GetDTO : class
        where ViewDTO : class
        where UpdateDTO : class
    {
        protected IHttpRequestClient _httpClient;
        protected readonly string _getByIdURL;
        protected readonly string _getPageURL;
        protected readonly string _updateURL;
        protected readonly string _updatePartialView;
        protected readonly string _viewPartialView;
        public ViewUpdateController(IHttpRequestClient httpClient, string baseURL, string viewPartialView, string updatePartialView)
        {
            _httpClient = httpClient;
            _getPageURL= $"{baseURL}/getpage";
            _getByIdURL = $"{baseURL}/getbyid/";
            _updateURL = $"{baseURL}/update/";
            _updatePartialView = updatePartialView;
            _viewPartialView = viewPartialView;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Update(string id)
        {
            var result = await _httpClient.GetAsync<UpdateDTO>($"{_getByIdURL}{id}" );
            return PartialView(_updatePartialView, result.Value);
        }

        [HttpGet]
        public virtual async Task<IActionResult> View(string id)
        {
            var result = await _httpClient.GetAsync<ViewDTO>($"{_getByIdURL}{id}" );
            return PartialView(_viewPartialView, result.Value);
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