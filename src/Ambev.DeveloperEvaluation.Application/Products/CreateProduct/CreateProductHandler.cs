using Ambev.DeveloperEvaluation.Common.Domain.Messages;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : CommandHandler, IRequestHandler<CreateProductCommand, CreateProductResult?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(DomainValidationContext domainNotificationContext,
        IProductRepository productRepository, IMapper mapper) : base(domainNotificationContext)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateProductResult?> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (!ValidCommand(command)) return null;

        var product = _mapper.Map<Product>(command);

        product.Id = Guid.NewGuid();

        _productRepository.Add(product);

        if (!await PersistData(_productRepository.UnitOfWork))
            return null;

        var result = _mapper.Map<CreateProductResult>(product);

        return result;
    }
}
