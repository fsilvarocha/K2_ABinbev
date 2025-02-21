namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductResponse
{
    public Guid Id { get; set; }
    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; set; }
}
