namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.AddOrRemoveItemSale;

/// <summary>
/// Represents a request to add an item to a sale. This class contains the necessary information
/// for identifying the sale and the product being added, as well as the quantity of the product.
/// </summary>
public class AddOrRemoveItemSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// This ID is used to link the product to a specific sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the product to be added to the sale.
    /// This ID is used to identify the product in the inventory system.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the sale.
    /// This indicates how many units of the product are being added to the sale.
    /// </summary>
    public int QuantityProduct { get; set; }
}

