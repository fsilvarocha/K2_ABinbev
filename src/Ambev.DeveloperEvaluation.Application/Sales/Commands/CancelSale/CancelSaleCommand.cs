using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;

public class CancelSaleCommand : Command<CancelSaleResult>
{
    public Guid SaleId { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new CancelSaleCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
