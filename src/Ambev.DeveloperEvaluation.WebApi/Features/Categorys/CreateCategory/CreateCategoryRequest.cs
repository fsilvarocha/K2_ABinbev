namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.CreateCategory;

/// <summary>
/// Represents a request to create a new category in the system.
/// </summary>
public class CreateCategoryRequest
{

    /// <summary>
    /// Get The Code for the Category
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Get name of the Category
    /// </summary>
    public string Name { get; set; }
}
