using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Companys.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{

    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().
            WithMessage("The property name cannot be empty");

        RuleFor(c => c.TaxId).NotEmpty().
             WithMessage("The property TaxId cannot be empty");

        RuleFor(c => c.Address).NotEmpty()
            .WithMessage("The property Address cannot be empty");
    }
}
