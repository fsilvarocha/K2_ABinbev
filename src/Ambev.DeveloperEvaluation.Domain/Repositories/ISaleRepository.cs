using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    public Task<Sale?> GetByIdAsync(Guid id);
    public Task<Sale?> GetWithSaleItemsByIdAsync(Guid id);
}
