using Ambev.DeveloperEvaluation.Application.Categorys.GetCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.GetCategory;

public class GetCategoryProfile : Profile
{
    public GetCategoryProfile()
    {
        CreateMap<GetCategoryRequest, GetCategoryCommand>();
        CreateMap<GetCategoryResult, GetCategoryResponse>();

        CreateMap<Guid, GetCategoryCommand>()
        .ConstructUsing(id => new GetCategoryCommand(id));
    }
}
