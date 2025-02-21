using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;

public class FinisheSaleCommandValidator : AbstractValidator<FinisheSaleCommand>
{
    public FinisheSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
           .NotEqual(Guid.Empty)
           .WithMessage("Invalid Sale ID");
    }
}
