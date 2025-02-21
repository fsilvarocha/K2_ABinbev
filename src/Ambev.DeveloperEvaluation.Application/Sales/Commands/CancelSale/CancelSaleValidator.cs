using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;

public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    public CancelSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
           .NotEqual(Guid.Empty)
           .WithMessage("Invalid Sale ID");
    }
}
