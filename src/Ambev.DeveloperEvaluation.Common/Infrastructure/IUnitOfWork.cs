namespace Ambev.DeveloperEvaluation.Common.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
