using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{

    const int MaxQuantityItems = 20;
    /// <summary>
    /// Indicates the manimum quantity of identical items allowed in the sale.
    /// </summary>
    const int MinQuantityIdenticalItemsDicount10Porcent = 5;

    const int MaxQuantityIdenticalItemsDicount10Porcent = 9;

    const int MinQuantityIdenticalItemsDicount20Porcent = 10;

    const int MaxQuantityIdenticalItemsDicount20Porcent = 20;



    /// <summary>
    /// Gets the identifier of client associated with the sale
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets the identifier of the company where the sale was made.
    /// </summary>
    public Guid CompanyId { get; set; }

    /// <summary>
    /// Gets the sale number 
    /// </summary>
    public long Number { get; set; }

    /// <summary>
    /// Gets the sale Status
    /// </summary>
    public SaleStatus Status { get; set; }

    public decimal AmountToPay { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets the date and time when sale was finished.
    /// </summary>
    public DateTime? FinalizedAt { get; set; }

    public List<SaleItem> SaleItems { get; set; } = [];

    /// <summary>
    /// Property mapping of EF Core.<br /> 
    /// Gets information about the client associated with the sale. 
    /// </summary>
    public User Client { get; set; }

    /// <summary>
    /// Property mapping of EF Core. <br /> 
    /// Gets information about the Company that made sale.
    /// </summary>
    public Company Company { get; set; }

    public void InitiateSale()
    {
        Status = SaleStatus.Initialized;
    }

    public void CancelSale()
    {
        Status = SaleStatus.Cancelled;
    }

    public void FinisheSale()
    {
        Status = SaleStatus.Finished;
    }

    private bool ExistsSaleItem(Guid productId)
    {
        return SaleItems.Any(si => si.ProductId == productId);
    }

    private SaleItem GetSaleItemById(Guid productId)
    {
        return SaleItems.FirstOrDefault(si => si.ProductId == productId);
    }

    /// <summary>
    /// Adds or updates a sale item. If the item already exists, updates the quantity; otherwise, adds a new item. 
    /// Validates sale restrictions and recalculates the sale value. 
    /// Returns an error message if restrictions are violated, or an empty string if successful.
    /// </summary>
    public string AddOrRemoveSaleItem(Guid productId, decimal price, int units)
    {
        var errorMessageMaxQuantityItemsExceeded = $"It is not possible to sell more than {MaxQuantityItems} identical products.";

        if (units == 0)
            return "It is not possible to add a product item with zero quantity.";

        if (ExistsSaleItem(productId))
        {
            var existingSaleItem = GetSaleItemById(productId);

            if (units > 0)
            {
                if (!CanAddUnits(existingSaleItem, units))
                    return errorMessageMaxQuantityItemsExceeded;

                existingSaleItem.AddUnits(units);
            }
            else
            {
                existingSaleItem.AddUnits(units);

                if (!existingSaleItem.HasUnits())
                    SaleItems.Remove(existingSaleItem);
            }

        }
        else
        {
            if (units < 0)
                return "It is not possible to add a product item with zero or negative quantity.";

            if (units > MaxQuantityItems) return errorMessageMaxQuantityItemsExceeded;

            var newSaleItem = CreateSaleItem(productId, price, units);
            SaleItems.Add(newSaleItem);
        }

        CalculateSaleValue();

        return string.Empty;
    }

    private bool CanAddUnits(SaleItem saleItem, int units)
    {
        int totalUnits = saleItem.Quantity + units;

        return totalUnits <= MaxQuantityItems;

    }

    private SaleItem CreateSaleItem(Guid productId, decimal price, int units)
    {
        return new SaleItem
        {
            SaleId = Id,
            ProductId = productId,
            Quantity = units,
            UnitPrice = price
        };
    }

    private void CalculateSaleValue()
    {
        TotalAmount = SaleItems.Sum(p => p.CalculateValue());

        CalculateTotalDiscountValue();
    }

    private void CalculateTotalDiscountValue()
    {

        decimal discountRate = GetDiscountRateValue();

        var discount = TotalAmount * discountRate;

        DiscountAmount = discount;

        AmountToPay = TotalAmount - discount;
    }

    private decimal GetDiscountRateValue()
    {

        var saleItemsWithHighestIdenticalMinimum = SaleItems.Where(x =>
            x.Quantity >= MinQuantityIdenticalItemsDicount10Porcent).ToList();

        if (!saleItemsWithHighestIdenticalMinimum.Any())
            return 0;

        var existsSaleItemsIntervalBetweenDicount20Porcent = saleItemsWithHighestIdenticalMinimum
             .Where(x => x.Quantity >= MinQuantityIdenticalItemsDicount20Porcent &&
             x.Quantity <= MaxQuantityIdenticalItemsDicount20Porcent).ToList();


        if (existsSaleItemsIntervalBetweenDicount20Porcent.Any())
            return 0.20M;


        return 0.10M;
    }

    public bool IsSaleActiveForModification()
    {
        var nonModifiableStatuses = new List<SaleStatus>
            {
                SaleStatus.Cancelled,
                SaleStatus.Finished
            };

        return !nonModifiableStatuses.Contains(Status);
    }

    public static class SaleFactory
    {
        public static Sale Initiate(Guid clientId, Guid companyId)
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                CompanyId = companyId,

            };

            sale.InitiateSale();

            return sale;
        }
    }
}
