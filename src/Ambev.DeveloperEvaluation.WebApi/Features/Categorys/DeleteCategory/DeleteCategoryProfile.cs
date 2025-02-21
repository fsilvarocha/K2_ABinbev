using Ambev.DeveloperEvaluation.Application.Categorys.DeleteCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categorys.DeleteCategory;

public class DeleteCategoryProfile : Profile
{
    public DeleteCategoryProfile()
    {
        CreateMap<Guid, DeleteCategoryCommand>()
            .ConstructUsing(id => new DeleteCategoryCommand(id));
    }
}
