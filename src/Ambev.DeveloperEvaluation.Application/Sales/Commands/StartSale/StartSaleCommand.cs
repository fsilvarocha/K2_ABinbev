using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;

public class StartSaleCommand : Command<StartSaleResult?>
{
    public Guid ClientId { get; set; }
    public Guid CompanyId { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new StartSaleCommandValidator();

        var result = validator.Validate(this);


        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public override bool IsValid()
    {
        ValidationResult = new StartSaleCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
