using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace STS.Core.Interface.Shared.IServices
{
    public interface ICrudWithPaginateService<Entity> : ICrudService<Entity> where Entity : class
    {
        Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>>? include = null);
    }
}
