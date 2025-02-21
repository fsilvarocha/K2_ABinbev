using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
           .WithMessage("The property name cannot be empty");

        RuleFor(c => c.Price).GreaterThan(0)
            .WithMessage("The property price must be greater than zero");
    }
}
