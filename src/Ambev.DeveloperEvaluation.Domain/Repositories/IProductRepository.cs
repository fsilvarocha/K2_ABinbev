using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    public Task<Product?> GetByIdAsync(Guid id);
}
