﻿using STS.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace STS.Core.Interfaces.Shared.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        T Find(params object[] keyValues);

        T FirstOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        T LastOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        T SingleOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        IQueryable<T> GetList(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,
            IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        IPagination<T> GetList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true);

        IQueryable<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
          Expression<Func<T, bool>>? predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
          bool disableTracking = true) where TResult : class;

        IPagination<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true) where TResult : class;

        bool Exist(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true);

        void Add(T entity);
        void Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(object id);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);
    }
}
