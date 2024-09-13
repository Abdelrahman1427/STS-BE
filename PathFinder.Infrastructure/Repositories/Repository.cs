
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using STS.SharedKernel.Interfaces;
using STS.Infrastructure.Extentions;
using STS.Core.Interfaces.Shared.Repository;

namespace STS.Infrastructure.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        #region Gets
        public IQueryable<T> Query(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public T Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public T FirstOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return query.FirstOrDefault();
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, 
            Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return query.Select(selector).FirstOrDefault();
        }

        public T LastOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).LastOrDefault() : query.LastOrDefault();
        }

        public T SingleOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).SingleOrDefault() : query.SingleOrDefault();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query) : query;
        }

        public IQueryable<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).Select(selector) : query.Select(selector);
        }

        public IPagination<T> GetList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0,
            int size = 20, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToPaginate(index, size) : query.ToPaginate(index, size);
        }

        public IPagination<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 20, bool disableTracking = true) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).ToPaginate(index, size)
                : query.Select(selector).ToPaginate(index, size);
        }

        public bool Exist(Expression<Func<T, bool>> predicate
            , Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
            , bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);

            return query.Count(predicate) > 0;
        }
        #endregion

        #region Curd
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(object id)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo)?.FindPrimaryKey()?.Properties.FirstOrDefault();
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

        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            var local = _dbContext.Set<T>()
           .Local
           .ToList();
            local.ForEach(x =>
                _dbContext.Entry(x).State = EntityState.Detached);
            var entry = _dbSet.Update(entity);
            var updated = entry.CurrentValues.Clone();
            entry.Reload();
            entry.CurrentValues.SetValues(updated);
            entry.State = EntityState.Modified;
            entry.State = EntityState.Detached;
        }

        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
        #endregion
    }
}