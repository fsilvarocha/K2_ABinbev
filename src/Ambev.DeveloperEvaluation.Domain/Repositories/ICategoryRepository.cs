using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<Category?> GetByCodeAsync(int code);
    public Task<Category?> GetByIdAsync(Guid Id);
}
