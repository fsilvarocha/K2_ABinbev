namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.AddOrRemoveItemSale;

public class AddOrRemoveItemSaleResponse
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

    public List<AddOrRemoveItemSaleItemResponse> SaleItems { get; set; }
}

public class AddOrRemoveItemSaleItemResponse
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

}

