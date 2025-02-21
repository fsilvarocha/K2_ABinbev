using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Common.Validation;

public class DomainValidationContext
{
    public readonly List<ValidationErrorDetail> Erros;

    public DomainValidationContext()
    {
        Erros = [];
    }

    public bool ExistErros => Erros.Any();

    public void AddValidationError(string error, string detail)
    {
        Erros.Add(new ValidationErrorDetail { Error = error, Detail = detail });
    }

    public void AddValidationErrors(List<ValidationFailure> validations)
    {
        Erros.AddRange(validations.Select(v => (ValidationErrorDetail)v));
    }

}
