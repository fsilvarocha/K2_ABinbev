using Ambev.DeveloperEvaluation.Application.Sales.Commands.FinisheSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.FinisheSale;

public class FinisheSaleProfile : Profile
{
    public FinisheSaleProfile()
    {
        CreateMap<FinisheSaleRequest, FinisheSaleCommand>();
    }
}
