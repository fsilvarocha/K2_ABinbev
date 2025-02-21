using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;

public class AddOrRemoveItemSaleCommandValidator : AbstractValidator<AddOrRemoveItemSaleCommand>
{
    public AddOrRemoveItemSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is requerid");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is requerid");

        RuleFor(x => x.QuantityProduct)
            .NotEqual(0)
            .WithMessage("The quantity must be greater than 0");
    }
}
