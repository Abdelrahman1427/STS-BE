using System.Linq.Expressions;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace PathFinder.Core.Interfaces.Shared.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        Task<T> FindAsync(params object[] keyValues);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true) where TResult : class;

        Task<T> LastOrDefaultAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true);

        Task<IEnumerable<IGrouping<object, T>>> GetListGroupingAsync(
            Expression<Func<T, object>> groupingKey,
            Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true);

        Task<IEnumerable<TResult>> GetListSelectorAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        Task<IEnumerable<TResult>> GetListGroupingAndSelectorAsync<TResult>(
            Expression<Func<IGrouping<object, T>, TResult>> selector,
            Expression<Func<T, object>> groupingKey,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default,
            SortingDTO? sorting = null);

        Task<IPagination<T>> GetListPaginitionAsync(Expression<Func<T, bool>>? predicate = null,
            SortingDTO? sorting = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0,
            int size = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<IGrouping<object, T>>> GetListGroupingPaginitionAsync(
             Expression<Func<T, object>> groupingKey,
             Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
             bool disableTracking = true, CancellationToken cancellationToken = default,
             int index = 0, int size = 25);

        Task<IPagination<TResult>> GetListSelectorPaginitionAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null, SortingDTO? sortingDTO = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TResult>> GetListSelectorAndGroupingPaginitionAsync<TResult>(
             Expression<Func<T, object>> groupingKey,
             Expression<Func<IGrouping<object, T>, TResult>> selector,
             Expression<Func<T, bool>>? predicate = null, SortingDTO? sorting = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
             bool disableTracking = true, CancellationToken cancellationToken = default,
             int index = 0, int size = 25);

        Task<int> CountAsync();

        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        void Update(T entity);

        public void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(object id);
        void Delete(IEnumerable<T> entities);
    }
}