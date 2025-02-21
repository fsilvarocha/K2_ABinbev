using Ambev.DeveloperEvaluation.Common.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly DomainValidationContext _domainValidationContext;

    public BaseController(DomainValidationContext domainValidationContext)
    {
        _domainValidationContext = domainValidationContext;
    }

    protected bool OperationValid()
    {
        return !_domainValidationContext.ExistErros;
    }

    protected ActionResult ErrorResponse()
    {
        var response = new ApiResponse
        {
            Success = false,
            Message = "Domain Validation Error",
            Errors = _domainValidationContext.Erros
        };

        return StatusCode(StatusCodes.Status400BadRequest, response);
    }

    protected int GetCurrentUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult Ok<T>(T data) =>
            base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Success = true
            });
}
