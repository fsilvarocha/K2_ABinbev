using Ambev.DeveloperEvaluation.Common.Infrastructure;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Common.Domain.Messages;

public abstract class CommandHandler
{
    protected readonly DomainValidationContext _domainValidationContext;

    protected CommandHandler(DomainValidationContext domainValidationContext)
    {
        _domainValidationContext = domainValidationContext;
    }

    protected bool ValidCommand<TResult>(Command<TResult> mensagem)
    {
        if (!mensagem.IsValid())
        {
            var result = mensagem.ValidationResult.Errors;
            _domainValidationContext.AddValidationErrors(result);

            return false;
        }
        return true;
    }

    protected void AddError(string error, string detail)
    {
        _domainValidationContext.AddValidationError(error, detail);
    }

    protected async Task<bool> PersistData(IUnitOfWork uow)
    {
        if (!await uow.Commit())
        {
            _domainValidationContext.AddValidationError("DataPersistenceError", "There was an error while persisting the data.");
            return false;
        }

        return true;
    }
}
