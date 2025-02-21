using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categorys.CreateCategory;

public class CreateCategoryHandler : CommandHandler, IRequestHandler<CreateCategoryCommand, CreateCategoryResult?>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(DomainValidationContext domainNotificationContext,
        ICategoryRepository categoryRepository, IMapper mapper) : base(domainNotificationContext)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;

    }

    public async Task<CreateCategoryResult?> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {

        if (!ValidCommand(command)) return null;

        var existsCategory = await _categoryRepository.GetByCodeAsync(command.Code);

        if (existsCategory != null)
        {
            AddError("Create Category Error", $"This code {command.Code} is " +
                $"already associated with a category.");

            return null;
        }

        var category = _mapper.Map<Category>(command);

        category.Id = Guid.NewGuid();

        _categoryRepository.Add(category);


        if (!await PersistData(_categoryRepository.UnitOfWork))
            return null;

        var result = _mapper.Map<CreateCategoryResult>(category);

        return result;
    }
}
