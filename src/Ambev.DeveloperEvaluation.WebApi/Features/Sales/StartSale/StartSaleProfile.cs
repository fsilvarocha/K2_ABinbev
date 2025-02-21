using Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.StartSale;

public class StartSaleProfile : Profile
{
    public StartSaleProfile()
    {
        CreateMap<StartSaleRequest, StartSaleCommand>();
        CreateMap<StartSaleResult, StartSaleResponse>();
    }
}
