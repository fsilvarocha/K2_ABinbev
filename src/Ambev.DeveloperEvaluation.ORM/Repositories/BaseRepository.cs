using Ambev.DeveloperEvaluation.Common.Infrastructure;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DefaultContext _context;

    public BaseRepository(DefaultContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(TEntity entity) =>
        _context.Set<TEntity>().Add(entity);

    public void Update(TEntity entity) =>
        _context.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity) =>
        _context.Set<TEntity>().Remove(entity);

    public void Dispose() =>
        _context?.Dispose();

}
