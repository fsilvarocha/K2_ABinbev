using Ambev.DeveloperEvaluation.Application.Sales.Commands.AddOrRemoveItemSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.AddOrRemoveItemSale;

public class AddOrRemoveItemSaleProfile : Profile
{
    public AddOrRemoveItemSaleProfile()
    {
        CreateMap<AddOrRemoveItemSaleRequest, AddOrRemoveItemSaleCommand>();
        CreateMap<AddOrRemoveItemSaleResult, AddOrRemoveItemSaleResponse>();
        CreateMap<AddOrRemoveItemSaleItemResult, AddOrRemoveItemSaleItemResponse>();
    }
}
