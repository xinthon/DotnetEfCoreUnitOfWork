namespace DotnetEfCoreUnitOfWork.Common.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}
