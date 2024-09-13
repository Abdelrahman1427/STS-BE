using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Models;
using PathFinder.WebApp.Clients;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.WebApp.Controllers
{
    public class AddViewDeleteController<AddDTO, GetDTO, ViewDTO> : Controller
        where AddDTO : class, new() 
        where GetDTO : class
         where ViewDTO : class
    {
        protected IHttpRequestClient _httpClient;
        protected readonly string _viewURL;
        protected readonly string _addURL;
        protected readonly string _getPageURL;
        protected readonly string _deleteURL;
        protected readonly string _addPartialView;
        protected readonly string _viewPartialView;
        public AddViewDeleteController(IHttpRequestClient httpClient, string getPageURL, string viewURL, string addURL, string addPartialView, string viewPartialView, string deleteURL = null)
        {
            _httpClient = httpClient;
            _getPageURL = getPageURL;
            _viewURL = viewURL;
            _addURL = addURL;
            _deleteURL = deleteURL;
            _addPartialView = addPartialView;
            _viewPartialView = viewPartialView;
        }
        public AddViewDeleteController(IHttpRequestClient httpClient, string baseURL, string addPartialView, string viewPartialView)
        {
            _httpClient = httpClient;
            _getPageURL = $"{baseURL}/getpage";
            _viewURL = $"{baseURL}/view/";
            _addURL = $"{baseURL}/add";
            _deleteURL = $"{baseURL}/delete/";
            _addPartialView = addPartialView;
            _viewPartialView = viewPartialView;
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
        public virtual async Task<IActionResult> View(string id)
        {
            var result = await _httpClient.GetAsync<ViewDTO>($"{_viewURL}{id}" );
            return PartialView(_viewPartialView, result.Value);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.DeleteAsync<APIResult>($"{_deleteURL}{id}" );
            response.Message = response.IsSuccess ? CoreResources.DeleteSuccessMessage : response.Message;
            return Json(response);
        }

        public virtual async Task<IActionResult> Index()
        {
            return View();
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
