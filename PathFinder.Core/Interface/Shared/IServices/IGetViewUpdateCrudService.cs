using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Interfaces;
using System.Linq.Expressions;

namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface IGetViewUpdateCrudService<Entity> where Entity : class
    {
        Task<bool> UpdateAsync(Entity entity);
        Task<Entity> GetByIdAsync(string id);
        Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto);

    }
}
