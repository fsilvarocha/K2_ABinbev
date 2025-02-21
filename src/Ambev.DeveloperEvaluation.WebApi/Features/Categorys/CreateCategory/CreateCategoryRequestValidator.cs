using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.CreateCategory;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("The property name cannot be empty");
    }
}
