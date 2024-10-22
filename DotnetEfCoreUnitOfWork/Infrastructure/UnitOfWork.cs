using DotnetEfCoreUnitOfWork.Common.Abstractions;
using DotnetEfCoreUnitOfWork.Infrastructure.Persistence;
using DotnetEfCoreUnitOfWork.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotnetEfCoreUnitOfWork.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new Repository<TEntity>(_context);
            _repositories[type] = repositoryInstance;
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
