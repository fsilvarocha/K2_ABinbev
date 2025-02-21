using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categorys.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("The property name cannot be empty");
    }
}
