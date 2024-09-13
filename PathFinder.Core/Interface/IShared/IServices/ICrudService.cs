using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace STS.Core.Interface.Shared.IServices
{
    public interface ICrudService<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> AddWithoutSaveAsync(Entity entity);
        Task<List<Entity>> AddWithoutSaveAsync(List<Entity> entities);
        Task<bool> UpdateAsync(Entity entity);
        Task<Entity> GetByIdAsync(int id);
        Task<Entity> GetUntrackedAsync(Expression<Func<Entity, bool>> predicate);
        Task<bool> DeleteAsync(int id);
        Task DeleteByEntity(Entity entity);
        Task<List<Entity>> GetLookUpAsync(Expression<Func<Entity, bool>> expression, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null);
    }
}
