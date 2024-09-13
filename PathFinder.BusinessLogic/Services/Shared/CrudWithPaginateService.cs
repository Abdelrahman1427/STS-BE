using STS.Core.Interfaces.Shared.Repository;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.SharedKernel.Constants;
using STS.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using STS.Core.Interface.Shared.IServices;


namespace STS.BusinessLogic.Services.Shared
{
    public class CrudWithPaginateService<Entity> : CrudService<Entity>, ICrudWithPaginateService<Entity>
       where Entity : class
    {
        public CrudWithPaginateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public virtual async Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>? include)
        {
            var entity = await _unitOfWork.GetRepositoryAsync<Entity>()
                                                              .GetListPaginitionAsync(
                                                                    predicate: expression,
                                                                    include: include,
                                                                    index: paginationDto.PageIndex,
                                                                    size: paginationDto.PageSize,
                                                                    sorting: paginationDto?.SortingDTO
                                                                  );
            return entity;
        }

        protected string? EmailOrPhone(string type)
        {
            if (type != null)
            {
                bool isPhone = type.All(char.IsDigit) && type.Length == 10 && type.StartsWith(AppConstants.PhoneType);
                bool isEmail = type.Contains('.') && type.Count(x => x == '@') == 1 && type.Count(x => x == '.') + 1 < type.Length;

                if (isPhone)
                    return AppConstants.PhoneNumber;

                else if (isEmail)
                    return AppConstants.Email;

                else
                    return AppConstants.UserName;
            }
            return null;
        }
    }

}
