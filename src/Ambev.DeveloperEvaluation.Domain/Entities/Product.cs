using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{

    public Guid CategoryId { get; set; }

    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the quantity of the product available in stock.
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Property mapping of EF Core.
    /// </summary
    public List<SaleItem> SaleItems { get; set; }

    public Category Category { get; set; }
}

