namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.StartSale;

/// <summary>
/// Represents the request to start a sale.
/// </summary>
public class StartSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the client initiating the sale.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the company associated with the sale.
    /// </summary>
    public Guid CompanyId { get; set; }
}
