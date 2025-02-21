using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleEventHandler : INotificationHandler<SaleCreatedEvent>, INotificationHandler<SaleModifiedEvent>, INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleEventHandler> _logger;

    public SaleEventHandler(ILogger<SaleEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SaleCreatedEvent executed, Sale ID = {notification.SaleId}");
        return Task.CompletedTask;
    }

    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SaleModifiedEvent executed, Sale ID = {notification.SaleId}");
        return Task.CompletedTask;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SaleCancelledEvent executed, Sale ID = {notification.SaleId}");
        return Task.CompletedTask;
    }


}
