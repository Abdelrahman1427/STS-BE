using System.Linq.Expressions;
using STS.Core.Interfaces.Shared.Repository;
using STS.SharedKernel.Constants;
using STS.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using STS.Core.Interface.Shared.IServices;


namespace STS.BusinessLogic.Services.Shared
{
    public class CrudService<Entity> : ICrudService<Entity>
        where Entity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CrudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            try
            {
                var _entity = _unitOfWork.GetRepositoryAsync<Entity>();
                await _entity.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("unique index"))
                    throw new STSException(ExceptionConstants.UniqueData);
                throw new STSException(ExceptionConstants.InternalServerError);
            }
        }

        public virtual async Task<Entity> AddWithoutSaveAsync(Entity entity)
        {
            try
            {
                var _entity = _unitOfWork.GetRepositoryAsync<Entity>();
                await _entity.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("unique index"))
                    throw new STSException(ExceptionConstants.UniqueData);
                throw new STSException(ExceptionConstants.InternalServerError);
            }
        }

        public virtual async Task<List<Entity>> AddWithoutSaveAsync(List<Entity> entities)
        {
            try
            {
                var _entity = _unitOfWork.GetRepositoryAsync<Entity>();
                await _entity.AddRangeAsync(entities);
                return entities;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("unique index"))
                    throw new STSException(ExceptionConstants.UniqueData);
                throw new STSException(ExceptionConstants.InternalServerError);
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = _unitOfWork.GetRepositoryAsync<Entity>();
                entity.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new STSException(ExceptionConstants.DeleteRestrictEntity);
            }
        }
        public virtual async Task DeleteByEntity(Entity entity)
        {
            try
            {
                var repo = _unitOfWork.GetRepositoryAsync<Entity>();
                repo.Delete(entity);
                int saved = await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new STSException(ExceptionConstants.DeleteRestrictEntity);
            }
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            var getEntity = _unitOfWork.GetRepositoryAsync<Entity>();
            var entity = await getEntity.FindAsync(id);
            return entity;

        }
        public virtual async Task<Entity> GetUntrackedAsync(Expression<Func<Entity, bool>> predicate)
        {
            var getEntity = _unitOfWork.GetRepositoryAsync<Entity>();
            var entity = await getEntity.FirstOrDefaultAsync(predicate: predicate, disableTracking: true);
            return entity;
        }
        
        public virtual async Task<bool> UpdateAsync(Entity entity)
        {
            if (entity == null)
                throw new STSException(ExceptionConstants.NullableUpdateElement);

            try
            {
                var _entity = _unitOfWork.GetRepositoryAsync<Entity>();
                _entity.Update(entity);
                int saved = await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.ToLower().Contains("unique index"))
                    throw new STSException(ExceptionConstants.UniqueData);
                throw new STSException(ExceptionConstants.InternalServerError);
            }
        }

        public virtual async Task<List<Entity>> GetLookUpAsync(Expression<Func<Entity, bool>> expression, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
        {
            var getAllEntity = await _unitOfWork.GetRepositoryAsync<Entity>().GetListAsync(predicate: expression, include: include);
            return getAllEntity.ToList();
        }
    }
}
