using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using STS.Core.Interfaces.Shared.Repository;

namespace STS.Infrastructure.Repositories
{
    public class UnitOfWork<T> : IRepositoryFactory, IUnitOfWork where T : DbContext , IDisposable
    {
        private Dictionary<Type, object> _repositories;
        public T Context { get; }

        public UnitOfWork(T context) 
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<TEntity>(Context);
            return (IRepositoryAsync<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int Complete()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
