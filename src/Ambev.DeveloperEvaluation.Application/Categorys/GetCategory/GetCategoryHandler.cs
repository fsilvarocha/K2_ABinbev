using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categorys.GetCategory;

public class GetCategoryHandler : CommandHandler, IRequestHandler<GetCategoryCommand, GetCategoryResult?>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryHandler(DomainValidationContext domainNotificationContext,
        ICategoryRepository categoryRepository, IMapper mapper) : base(domainNotificationContext)
    {
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
    }

    public async Task<GetCategoryResult?> Handle(GetCategoryCommand commmand, CancellationToken cancellationToken)
    {
        if (!ValidCommand(commmand)) return null;

        var category = await _categoryRepository.GetByIdAsync(commmand.Id);

        if (category == null)
        {
            AddError("Get Category Error", $"Category with ID " +
                $"{commmand.Id} not found");

            return null;
        }

        return _mapper.Map<GetCategoryResult>(category);
    }
}
