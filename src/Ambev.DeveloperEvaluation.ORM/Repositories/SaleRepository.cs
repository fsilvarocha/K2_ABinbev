using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {

    }

    public async Task<Sale?> GetByIdAsync(Guid id) =>
        await _context.Set<Sale>().FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Sale?> GetWithSaleItemsByIdAsync(Guid id) =>
        await _context.Set<Sale>().Include(s => s.SaleItems)
            .ThenInclude(si => si.Product).FirstOrDefaultAsync(s => s.Id == id);

}
