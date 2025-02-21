using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DefaultContext context) : base(context) { }

    public async Task<Category?> GetByCodeAsync(int code) =>
        await _context.Categorys
            .FirstOrDefaultAsync(c => c.Code == code);

    public async Task<Category?> GetByIdAsync(Guid Id) =>
        await _context.Categorys.FirstOrDefaultAsync(c => c.Id == Id);

}
