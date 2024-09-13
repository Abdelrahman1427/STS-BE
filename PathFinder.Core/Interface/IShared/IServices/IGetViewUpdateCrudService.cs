using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.SharedKernel.Interfaces;
using System.Linq.Expressions;

namespace STS.Core.Interface.Shared.IServices
{
    public interface IGetViewUpdateCrudService<Entity> where Entity : class
    {
        Task<bool> UpdateAsync(Entity entity);
        Task<Entity> GetByIdAsync(string id);
        Task<IPagination<Entity>> GetPageAsync(Expression<Func<Entity, bool>> expression, PagingDTO paginationDto);

    }
}
