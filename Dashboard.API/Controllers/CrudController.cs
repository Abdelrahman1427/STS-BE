using AutoMapper;
using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Filters;
using Microsoft.AspNetCore.Mvc;
using STS.Core.Interface.Shared.IServices;


namespace STS.API.Controllers
{
    public class CrudController<TEntity,TAddDTO, TUpdateDTO, TGetByIdDTO, TGetPageDTO> : ControllerBase
        where TEntity : class
        where TAddDTO : class
        where TUpdateDTO : class
        where TGetByIdDTO : class
        where TGetPageDTO : class
    {
        private IMapper _mapper;
        private ICrudService<TEntity> _service;
        public CrudController(IMapper mapper, ICrudService<TEntity> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("Add")]
        [LoggerAction]
        public virtual async Task<APIResult> Add(TAddDTO addDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new APIResult { state = false, entity = ModelState };
                var _mapperDto = _mapper.Map<TEntity>(addDTO);
                await _service.AddAsync(_mapperDto);
                var result = _mapper.Map<TAddDTO>(_mapperDto);
                return new APIResult { state = true, entity = result };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpPut("Update/{id}")]
        [LoggerAction]
        public virtual async Task<APIResult> Update(int id, TUpdateDTO updateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new APIResult { state = false, entity = ModelState };
                var entity = await _service.GetByIdAsync(id);
                _mapper.Map(updateDTO, entity);
                await _service.UpdateAsync(entity);
                return new APIResult { state = true, entity = updateDTO };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpDelete("Delete/{id}")]
        [LoggerAction]
        public virtual async Task<APIResult> Delete(int id)
        {
            try
            {
                Result result = new Result();
                result.Status = await _service.DeleteAsync(id);
                return new APIResult { state = true, entity = result };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        [HttpGet("GetById/{id}")]
        [LoggerAction]
        public virtual async Task<APIResult> GetById(int id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);
                var result = _mapper.Map<TGetByIdDTO>(data);
                return new APIResult { state = true, entity = result };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
            
        }
    }
}
