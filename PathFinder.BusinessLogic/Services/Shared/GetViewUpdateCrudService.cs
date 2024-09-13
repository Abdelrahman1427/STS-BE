using STS.Core.Interfaces.Shared.Repository;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.SharedKernel.Constants;
using STS.SharedKernel.Exceptions;
using STS.SharedKernel.Interfaces;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using STS.Core.Interface.Shared.IServices;

namespace STS.BusinessLogic.Services.Shared
{
    public class GetViewUpdateCrudService<Entity> : IGetViewUpdateCrudService<Entity>
          where Entity : class
    {
        protected IUnitOfWork _unitOfWork;
        public GetViewUpdateCrudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual async Task<Entity> GetByIdAsync(string id)
        {
            var _entity = _unitOfWork.GetRepositoryAsync<Entity>();
            var entity = await _entity.FindAsync(id);
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(Entity entity)
        {
            if (entity == null)
                throw new STSException(ExceptionConstants.NullableUpdateElement);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto)
        {
            var entity = await _unitOfWork.GetRepositoryAsync<Entity>()
                                                              .GetListPaginitionAsync(
                                                                    predicate: expression,
                                                                    index: paginationDto.PageIndex,
                                                                    size: paginationDto.PageSize,
                                                                    sorting: paginationDto?.SortingDTO
                                                                  );
            return entity;
        }
    }
}