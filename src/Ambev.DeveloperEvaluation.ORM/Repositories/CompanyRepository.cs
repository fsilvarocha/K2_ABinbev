using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(DefaultContext context) : base(context)
    {
    }

    public async Task<Company?> GetByIdAsync(Guid Id) =>
        await _context.Companys.FirstOrDefaultAsync(c => c.Id == Id);
}
