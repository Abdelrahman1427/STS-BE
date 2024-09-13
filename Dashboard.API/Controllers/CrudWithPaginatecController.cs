using AutoMapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Filters;
using PathFinder.Core.Interface.Shared.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PathFinder.Core.Entities;


namespace Dashboard.API.Controllers
{
    public class CrudWithPaginateController<TEntity, TAddDTO, TUpdateDTO, TGetByIdDTO, TGetPageDTO> : CrudController<TEntity, TAddDTO, TUpdateDTO, TGetByIdDTO, TGetPageDTO>
        where TEntity : class
        where TAddDTO : class
        where TUpdateDTO : class
        where TGetByIdDTO : class
        where TGetPageDTO : class
    {
        private IMapper _mapper;
        private ICrudWithPaginateService<TEntity> _service;
        protected Expression<Func<TEntity, bool>> _predicate;
        protected Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> _include;
        public CrudWithPaginateController(IMapper mapper, ICrudWithPaginateService<TEntity> service) :base(mapper, service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("GetPage")]
        [LoggerAction]
        public virtual async Task<APIResult> GetPage(PagingDTO paginationDTO)
        {
            try
            {
                var data = await _service.GetPageAsync(_predicate, paginationDTO, _include);
                var result = _mapper.Map<Pagination<TGetPageDTO>>(data);
                return new APIResult { state = true , entity = result };
            }
            catch (Exception ex)                         
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpGet("GetLookUp")]
        [LoggerAction]
        public virtual async Task<IActionResult> GetLookUp()
        {
            var data = await _service.GetLookUpAsync(_predicate);
            var result = _mapper.Map<List<GetLookUpDefinitionDTO>>(data);
            return Ok(result);
        }
    }
}
