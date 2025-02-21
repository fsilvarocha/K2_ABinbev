using Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.AddOrRemoveItemSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.FinisheSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.StartSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public SalesController(DomainValidationContext domainValidationContext,
        IMediator mediator, IMapper mapper) : base(domainValidationContext)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<StartSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartSale([FromBody] StartSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new StartSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<StartSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Created(string.Empty, new ApiResponseWithData<StartSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<StartSaleResponse>(response)
        });
    }
    [HttpPost("items")]
    [ProducesResponseType(typeof(ApiResponseWithData<StartSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrRemoveItemSale([FromBody] AddOrRemoveItemSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new AddOrRemoveItemSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<AddOrRemoveItemSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Created(string.Empty, new ApiResponseWithData<AddOrRemoveItemSaleResponse>
        {
            Success = true,
            Message = "Item successfully added or removed.",
            Data = _mapper.Map<AddOrRemoveItemSaleResponse>(response)
        });
    }


    [HttpPost("cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel([FromBody] CancelSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CancelSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale Cancelled successfully"
        });
    }


    [HttpPost("finishe")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Finishe([FromBody] FinisheSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new FinisheSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<FinisheSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale Finished successfully"
        });
    }
}
