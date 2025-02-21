namespace Ambev.DeveloperEvaluation.Application.Products.Queries.DTOs;

//public class ListProductsResponseDTO
//{
//    public PaginatedList<ListProductsItemResponseDTO> ListProductsItemResponse { get; set; }
//}

public class ListProductsItemResponseDTO
{
    /// <summary>
    /// Gets name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets the Category of the product.
    /// </summary>
    public string Category { get; set; }
}
