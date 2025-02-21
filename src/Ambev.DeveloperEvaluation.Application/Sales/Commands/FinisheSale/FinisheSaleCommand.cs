using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;

public class FinisheSaleCommand : Command<FinisheSaleResult>
{
    public Guid SaleId { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new FinisheSaleCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }

}
