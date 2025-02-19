using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    [IgnoreDataMember]
    public decimal TotalAmount => Quantity * UnitPrice - Discount;
    [JsonIgnore]
    public Guid SaleId { get; private set; }
    [JsonIgnore]
    public Sale Sale { get; private set; }

    public SaleItem(string productName, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new Exception("Não é permitido vender mais de 20 itens idênticos.");

        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = 0;
    }

    public void ApplyDiscount(decimal discountPercentage)
    {
        Discount = (Quantity * UnitPrice) * discountPercentage;
    }

    public void RemoveDiscount()
    {
        Discount = 0;
    }
}
