using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.FinisheSale;

public class FinisheSaleRequestValidator : AbstractValidator<FinisheSaleRequest>
{
    public FinisheSaleRequestValidator()
    {
        RuleFor(x => x.SaleId)
        .NotEmpty()
        .WithMessage("Sale ID is required");
    }
}
