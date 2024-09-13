using Microsoft.AspNetCore.Mvc;
using PathFinder.WebApp.Clients;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
using Newtonsoft.Json;
using PathFinder.SharedKernel.Authorize;

namespace PathFinder.WebApp.Controllers
{
    public class IndexCrudController<GetDTO, AddDTO, UpdateDTO> : CrudController<AddDTO, UpdateDTO>
        where GetDTO : class, new() where AddDTO : class, new() where UpdateDTO : class
    {
        protected readonly string _getPageURL;
        public IndexCrudController(IHttpRequestClient httpClient, string getByIdURL, string addURL, string updateURL, string addPartialView, string updatePartialView, string getPageURL = null, string deleteURL = null) : base(httpClient, getByIdURL, addURL, updateURL, addPartialView, updatePartialView, deleteURL)
        {
            _getPageURL = getPageURL;
        }
        public IndexCrudController(IHttpRequestClient httpClient, string baseURL, string addPartialView, string updatePartialView) : base(httpClient, baseURL, addPartialView, updatePartialView)
        {
            _getPageURL = $"{baseURL}/getpage";
        }

        [DynamicAuthorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [DynamicAuthorize]
        public virtual async Task<JsonResult> LoadDataTable(DataTableModel data)
        {
            var page = new PagingDTO()
            {
                PageIndex = data.PageNo,
                PageSize = data.Length,
                SortingDTO = new SortingDTO
                {
                    IsOrderAsc = data.Order[0].Dir == "asc" ? true : false,
                    OrderBy = data.Columns[data.Order[0].Column].Name,
                }
            };
            var result = await _httpClient.PostAsync<Pagination<GetDTO>, PagingDTO>(_getPageURL, page );
            Pagination<GetDTO> getLookUpDefinitionDTO = JsonConvert.DeserializeObject<Pagination<GetDTO>>(result.Value.entity?.ToString())!;
            return Json(data.GetDataTableModel(getLookUpDefinitionDTO));
        }
    } 
}
    