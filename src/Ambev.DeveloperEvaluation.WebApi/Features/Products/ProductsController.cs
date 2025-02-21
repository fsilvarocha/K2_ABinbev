using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Queries;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductQueries _productQueries;

    public ProductsController(DomainValidationContext domainValidationContext,
        IMediator mediator, IMapper mapper, IProductQueries productQueries) : base(domainValidationContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productQueries = productQueries;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }
}
