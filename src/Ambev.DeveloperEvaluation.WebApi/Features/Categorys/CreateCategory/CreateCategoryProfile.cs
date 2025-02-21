using Ambev.DeveloperEvaluation.Application.Categorys.CreateCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.CreateCategory;

public class CreateCategoryProfile : Profile
{
    public CreateCategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
        CreateMap<CreateCategoryResult, CreateCategoryResponse>();
    }
}
