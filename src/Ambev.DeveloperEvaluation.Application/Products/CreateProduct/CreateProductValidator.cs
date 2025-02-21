using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("The property name cannot be empty");

        RuleFor(c => c.Price).GreaterThan(0)
            .WithMessage("The property price must be greater than zero");
    }
}
