using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.DeleteCategory;

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Category ID is required");
    }
}
