using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Categorys.DeleteCategory;

/// <summary>
/// Represents the command to delete a category.
/// </summary>
public class DeleteCategoryCommand : Command<DeleteCategoryResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the category to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new DeleteCategoryCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
