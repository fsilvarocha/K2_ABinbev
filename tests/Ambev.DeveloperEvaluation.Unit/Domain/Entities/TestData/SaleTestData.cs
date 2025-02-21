using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleTestData
{
    public static Sale GenerateValidSale()
    {
        Guid saleId = Guid.NewGuid();

        return new Sale
        {
            Id = saleId,
            ClientId = Guid.NewGuid(),
            CompanyId = Guid.NewGuid(),
            Status = SaleStatus.Initialized,
            TotalAmount = 0,
            DiscountAmount = 0,
            AmountToPay = 0,
        };
    }
}
