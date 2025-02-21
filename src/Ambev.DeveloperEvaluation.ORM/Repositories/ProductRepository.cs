using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DefaultContext context) : base(context)
    {
    }

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
}
