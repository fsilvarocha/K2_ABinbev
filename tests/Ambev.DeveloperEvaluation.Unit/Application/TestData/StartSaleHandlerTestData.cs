using Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class StartSaleHandlerTestData
{
    private static readonly Faker<StartSaleCommand> createStartSaleHandlerFaker = new Faker<StartSaleCommand>()
        .RuleFor(x => x.ClientId, x => x.Random.Guid())
        .RuleFor(x => x.CompanyId, x => x.Random.Guid());

    public static StartSaleCommand GenerateValidCommand() =>
        createStartSaleHandlerFaker.Generate();
}
