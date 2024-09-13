using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface ICrudWithPaginateService<Entity> : ICrudService<Entity> where Entity : class
    {
        Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>? include = null);
    }
}
