namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.StartSale;

/// <summary>
/// Represents the response when starting a sale.
/// </summary>
public class StartSaleResponse
{
    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public int Number { get; set; }
}
