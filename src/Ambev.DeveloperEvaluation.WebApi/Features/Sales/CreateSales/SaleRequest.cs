using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateItemSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class SaleRequest
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; }
    public string Branch { get; set; }
    public List<SaleItemRequest> Items { get; set; }
}
