using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;

public class StartSaleCommandValidator : AbstractValidator<StartSaleCommand>
{

    public StartSaleCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client ID");

        RuleFor(x => x.ClientId)
           .NotEqual(Guid.Empty)
           .WithMessage("Invalid company ID");
    }
}
