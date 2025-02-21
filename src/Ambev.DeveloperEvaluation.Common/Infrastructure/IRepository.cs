namespace Ambev.DeveloperEvaluation.Common.Infrastructure;

public interface IRepository<TEntity> : IDisposable
{
    IUnitOfWork UnitOfWork { get; }

    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);


}
