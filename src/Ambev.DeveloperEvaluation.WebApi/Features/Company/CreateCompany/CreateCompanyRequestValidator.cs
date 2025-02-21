using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(c => c.Name).NotEmpty().
            WithMessage("The property name cannot be empty");

        RuleFor(c => c.TaxId).NotEmpty().
             WithMessage("The property TaxId cannot be empty");

        RuleFor(c => c.Address).NotEmpty()
            .WithMessage("The property Address cannot be empty");
    }
}
