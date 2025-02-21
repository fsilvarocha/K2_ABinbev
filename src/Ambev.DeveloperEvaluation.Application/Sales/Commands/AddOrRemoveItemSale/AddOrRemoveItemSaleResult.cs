namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;

/// <summary>
/// Represents the result of adding an item to a sale.
/// </summary>
public class AddOrRemoveItemSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the amount to pay for the sale after applying discounts.
    /// </summary>
    public decimal AmountToPay { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the sale before applying discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the total discount amount applied to the sale.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the sale.
    /// </summary>
    public List<AddOrRemoveItemSaleItemResult> SaleItems { get; set; }
}

/// <summary>
/// Represents a single item included in the sale.
/// </summary>
public class AddOrRemoveItemSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product included in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}
