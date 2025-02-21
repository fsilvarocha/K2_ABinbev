using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Categorys.GetCategory;

public class GetCategoryCommand : Command<GetCategoryResult?>
{
    public Guid Id { get; set; }

    public GetCategoryCommand(Guid id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new GetCategoryCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }

}
