using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.Infrastructure.Extentions;
using PathFinder.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Asn1;


namespace PathFinder.Infrastructure.Repositories
{
    internal class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #region Gets
        public IQueryable<T> Query(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public async Task<T> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<T> LastOrDefaultAsync(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return await query.LastOrDefaultAsync();
        }  

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<IGrouping<object, T>>> GetListGroupingAsync(
            Expression<Func<T, object>> groupingKey,
            Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.GroupBy(groupingKey).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetListSelectorAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.Select(selector).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetListGroupingAndSelectorAsync<TResult>(
            Expression<Func<IGrouping<object, T>, TResult>> selector,
            Expression<Func<T, object>> groupingKey,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default,
            SortingDTO? sorting = null)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.GroupBy(groupingKey)
                .Select(selector).ToListAsync(cancellationToken);
        }


        public async Task<IPagination<T>> GetListPaginitionAsync(Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0,
            int size = 20, bool disableTracking = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.ToPaginateAsync(index, size, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<IGrouping<object, T>>> GetListGroupingPaginitionAsync(
             Expression<Func<T, object>> groupingKey,
             Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
             bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken),
             int index = 0, int size = 25)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.GroupBy(groupingKey)
                .Skip(index * size)
                .Take(size).ToListAsync(cancellationToken);
        }

        public async Task<IPagination<TResult>> GetListSelectorPaginitionAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return  await query.Select(selector).ToPaginateAsync(index, size, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetListSelectorAndGroupingPaginitionAsync<TResult>(
             Expression<Func<T, object>> groupingKey,
             Expression<Func<IGrouping<object, T>, TResult>> selector,
             Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
             bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken),
             int index = 0, int size = 25)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (sorting != null) query = query.ToSort(sorting.OrderBy, sorting.IsOrderAsc);

            return await query.GroupBy(groupingKey)
                .Select(selector)
                .Skip(index * size)
                .Take(size).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate
            , Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            , bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);

            return await query.CountAsync(predicate) > 0;
        }
        #endregion


        #region Curd
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null) Delete(entity);
            }
        }
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        #endregion
    }
}