using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.StartSale;

public class StartSaleRequestValidator : AbstractValidator<StartSaleRequest>
{
    public StartSaleRequestValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("Client ID is required");

        RuleFor(x => x.CompanyId)
            .NotEmpty()
            .WithMessage("Company ID is required");
    }
}
