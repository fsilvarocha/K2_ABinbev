using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.StartSale;

public class StartSaleProfile : Profile
{
    public StartSaleProfile()
    {
        CreateMap<Sale, StartSaleResult>();
    }
}
