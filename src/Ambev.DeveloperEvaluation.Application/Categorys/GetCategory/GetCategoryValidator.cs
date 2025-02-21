using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categorys.GetCategory;

public class GetCategoryCommandValidator : AbstractValidator<GetCategoryCommand>
{
    public GetCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Category ID is required");
    }
}
