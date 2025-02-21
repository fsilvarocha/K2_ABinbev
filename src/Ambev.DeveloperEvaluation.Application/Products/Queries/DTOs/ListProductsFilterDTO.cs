using Ambev.DeveloperEvaluation.Common.Application.Objects;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.DTOs;

public class ListProductsFilterDTO : FilterParameter
{

    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets name of the category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the minimum price for filtering or validation purposes.
    /// </summary>
    public decimal? MinPrice { get; set; }
    /// <summary>
    /// Gets or sets the maximum price for filtering or validation purposes.
    /// </summary>
    public decimal? MaxPrice { get; set; }


}
