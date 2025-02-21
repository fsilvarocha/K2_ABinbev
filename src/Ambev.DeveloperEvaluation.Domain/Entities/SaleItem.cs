using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    /// <summary>
    /// Gets the identifier of sale associated with the sale item
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the identifier of product associated with the sale item
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets quantity of products
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///Gets unit price of product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Property mapping of EF Core.<br /> 
    /// </summary
    public Sale Sale { get; set; }

    /// <summary>
    /// Property mapping of EF Core.<br /> 
    /// </summary
    public Product Product { get; set; }

    public decimal CalculateValue()
    {
        return Quantity * UnitPrice;
    }

    public bool HasUnits()
    {
        return Quantity > 0;
    }

    public void AddUnits(int units)
    {
        Quantity += units;
    }

    public void UpdateUnits(int units)
    {
        Quantity = units;
    }


}