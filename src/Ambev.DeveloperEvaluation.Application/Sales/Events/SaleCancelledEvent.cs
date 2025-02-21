using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleCancelledEvent : Event
{
    public Guid SaleId { get; set; }

    public SaleCancelledEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
