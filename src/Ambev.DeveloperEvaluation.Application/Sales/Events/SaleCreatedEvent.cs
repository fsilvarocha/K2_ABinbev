using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleCreatedEvent : Event
{
    public Guid SaleId { get; set; }

    public SaleCreatedEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
