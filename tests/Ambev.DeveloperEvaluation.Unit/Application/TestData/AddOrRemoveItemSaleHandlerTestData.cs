using Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class AddOrRemoveItemSaleHandlerTestData
{
    public static AddOrRemoveItemSaleCommand GeneratedValidCommand()
    {
        return new AddOrRemoveItemSaleCommand
        {
            ProductId = Guid.NewGuid(),
            SaleId = Guid.NewGuid(),
            QuantityProduct = 1
        };
    }
    public static AddOrRemoveItemSaleCommand GeneratedInvalidCommand()
    {
        return new AddOrRemoveItemSaleCommand
        {
            ProductId = Guid.NewGuid(),
            SaleId = Guid.NewGuid(),
            QuantityProduct = 0
        };
    }
}
