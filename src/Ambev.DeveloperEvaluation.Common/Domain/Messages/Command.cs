using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Common.Domain.Messages;

public class Command<TResult> : IRequest<TResult>
{
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {

    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}
