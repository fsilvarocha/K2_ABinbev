using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Category : BaseEntity
{

    /// <summary>
    /// Get The Code for the Category
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Get name of the Category
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Property mapping of EF Core.
    /// Gets the list products for the category
    /// </summary>
    public List<Product> Products { get; set; }
}
