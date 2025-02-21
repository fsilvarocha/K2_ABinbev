using Ambev.DeveloperEvaluation.Application.Companys.CreateCompany;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company;

public class CompaniesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CompaniesController(DomainValidationContext domainValidationContext,
        IMediator mediator, IMapper mapper) : base(domainValidationContext)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCompanyResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCompanyRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCompanyCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!OperationValid())
            return ErrorResponse();

        return Created(string.Empty, new ApiResponseWithData<CreateCompanyResponse>
        {
            Success = true,
            Message = "Company created successfully",
            Data = _mapper.Map<CreateCompanyResponse>(response)
        });
    }
}
