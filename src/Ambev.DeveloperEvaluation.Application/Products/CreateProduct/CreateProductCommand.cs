using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommand : Command<CreateProductResult>
{

    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new CreateProductCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
