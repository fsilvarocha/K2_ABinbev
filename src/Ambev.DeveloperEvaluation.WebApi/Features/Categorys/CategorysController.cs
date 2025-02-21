using Ambev.DeveloperEvaluation.Application.Categorys.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Categorys.DeleteCategory;
using Ambev.DeveloperEvaluation.Application.Categorys.GetCategory;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Categorys.CreateCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categorys.DeleteCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categorys.GetCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys;

[ApiController]
[Route("api/[controller]")]
public class CategorysController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CategorysController(DomainValidationContext domainValidationContext,
        IMediator mediator, IMapper mapper) : base(domainValidationContext)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCategoryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCategoryCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Created(string.Empty, new ApiResponseWithData<CreateCategoryResponse>
        {
            Success = true,
            Message = "Category created successfully",
            Data = _mapper.Map<CreateCategoryResponse>(response)
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCategoryRequest { Id = id };
        var validator = new GetCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCategoryCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Ok(new ApiResponseWithData<GetCategoryResponse>
        {
            Success = true,
            Message = "User retrieved successfully",
            Data = _mapper.Map<GetCategoryResponse>(response)
        });
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryRequest { Id = id };
        var validator = new DeleteCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCategoryCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Category deleted successfully"
        });
    }

}
