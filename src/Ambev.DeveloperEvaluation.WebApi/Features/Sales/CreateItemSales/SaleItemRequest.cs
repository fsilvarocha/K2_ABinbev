namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateItemSales;

public class SaleItemRequest
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
