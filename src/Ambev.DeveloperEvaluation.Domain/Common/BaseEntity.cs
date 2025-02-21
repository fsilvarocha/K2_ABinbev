using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }

    private List<Event> _notifications;
    public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public void AddEvent(Event evento)
    {
        _notifications = _notifications ?? new List<Event>();
        _notifications.Add(evento);
    }

    public void ClearEvents()
    {
        _notifications?.Clear();
    }
}
