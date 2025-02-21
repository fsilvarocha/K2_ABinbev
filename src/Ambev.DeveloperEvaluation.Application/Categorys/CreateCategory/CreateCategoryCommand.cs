using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Categorys.CreateCategory;

public class CreateCategoryCommand : Command<CreateCategoryResult>
{
    /// <summary>
    /// Get The Code for the Category
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Get name of the Category
    /// </summary>
    public string Name { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new CreateCategoryCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
