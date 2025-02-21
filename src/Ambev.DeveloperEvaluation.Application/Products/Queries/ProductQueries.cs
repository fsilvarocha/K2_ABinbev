using Ambev.DeveloperEvaluation.Application.Products.Queries.DTOs;
using Ambev.DeveloperEvaluation.Common.Utils;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries;

public class ProductQueries : IProductQueries
{
    private readonly DefaultContext _context;

    public ProductQueries(DefaultContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ListProductsItemResponseDTO>> ListProductsAsync(ListProductsFilterDTO filter)
    {

        var query = _context.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = DynamicQueryUtils.FilterStringWithAsterisk(query, filter.Name, p => p.Name);

        if (filter.Price.HasValue)
            query = query.Where(p => p.Price == filter.Price.Value);

        if (!string.IsNullOrEmpty(filter.Category))
            query = DynamicQueryUtils.FilterStringWithAsterisk(query, filter.Category, p => p.Category.Name);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);


        if (!string.IsNullOrEmpty(filter.OrderBy))
            query = DynamicQueryUtils.ApplyOrderByString(query, filter.OrderBy);

        Func<IQueryable<Product>, IQueryable<ListProductsItemResponseDTO>> projection =
            q => q.Select(p => new ListProductsItemResponseDTO
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category.Name
            });

        return await PaginatedList<Product>.CreateAsync<Product, ListProductsItemResponseDTO>(query,
            filter.PageNumber, filter.PageSize, projection);
    }
}
