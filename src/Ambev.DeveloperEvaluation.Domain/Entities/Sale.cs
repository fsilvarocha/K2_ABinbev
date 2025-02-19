namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; }
    public decimal TotalAmount { get; private set; }
    public string Branch { get; set; }
    public ICollection<SaleItem> Items { get; private set; } =[];
    public bool IsCancelled { get; private set; }

    public Sale(string saleNumber, DateTime saleDate, string customer, string branch)
    {
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        Customer = customer;
        Branch = branch;
    }

    public void AddItem(SaleItem item)
    {
        if (Items.Any(x => x.ProductName == item.ProductName && x.Quantity + item.Quantity > 20))
            throw new Exception("Do not allowed sales more than 20 same items.");

        Items.Add(item);
        ApplyDiscounts();
        CalculateTotal();
    }

    public void CancelSale()
    {
        IsCancelled = true;
    }

    public void CalculateTotal()
    {
        ApplyDiscounts();
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }

    private void ApplyDiscounts()
    {
        foreach (var item in Items)
        {
            if (item.Quantity >= 10 && item.Quantity <= 20)
            {
                item.ApplyDiscount(0.20m);
            }
            else if (item.Quantity >= 4)
            {
                item.ApplyDiscount(0.10m);
            }
            else
            {
                item.RemoveDiscount();
            }
        }
    }
}
