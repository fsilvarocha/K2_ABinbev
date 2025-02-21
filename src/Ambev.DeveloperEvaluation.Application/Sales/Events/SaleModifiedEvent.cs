using Ambev.DeveloperEvaluation.Common.Domain.Messages;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleModifiedEvent : Event
{
    public Guid SaleId { get; set; }
    public SaleModifiedEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
