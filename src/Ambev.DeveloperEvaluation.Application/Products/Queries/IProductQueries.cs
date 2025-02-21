using Ambev.DeveloperEvaluation.Application.Products.Queries.DTOs;
using Ambev.DeveloperEvaluation.Common.Utils;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries;

public interface IProductQueries
{
    Task<PaginatedList<ListProductsItemResponseDTO>> ListProductsAsync(ListProductsFilterDTO filter);
}
