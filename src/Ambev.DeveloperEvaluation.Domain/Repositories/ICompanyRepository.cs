using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICompanyRepository : IRepository<Company>
{
    public Task<Company?> GetByIdAsync(Guid Id);
}
