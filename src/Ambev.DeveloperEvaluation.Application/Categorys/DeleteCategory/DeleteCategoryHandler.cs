using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categorys.DeleteCategory;

public class DeleteCategoryHandler : CommandHandler, IRequestHandler<DeleteCategoryCommand, DeleteCategoryResult?>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(DomainValidationContext domainNotificationContext,
        ICategoryRepository categoryRepository) : base(domainNotificationContext)
    {
        _categoryRepository = categoryRepository;

    }
    public async Task<DeleteCategoryResult?> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        if (!ValidCommand(command)) return null;

        var existsCategory = await _categoryRepository.GetByIdAsync(command.Id);

        if (existsCategory == null)
        {
            AddError("Delete Category", $"Category with ID {command.Id} not found");
            return null;
        }

        _categoryRepository.Delete(existsCategory);

        if (!await PersistData(_categoryRepository.UnitOfWork))
            return null;

        return new DeleteCategoryResult { Success = true };
    }
}
