namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;

public class StartSaleResult
{
    /// <summary>
    /// Gets the unique identifier of the newly created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the sale number 
    /// </summary>
    public int Number { get; set; }
}
