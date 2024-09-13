namespace PathFinder.Core.Interfaces.Shared.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
        public int Complete();
    }
}
