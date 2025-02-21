using Ambev.DeveloperEvaluation.Application.Companys.CreateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;

public class CreateCompanyProfile : Profile
{
    public CreateCompanyProfile()
    {
        CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
        CreateMap<CreateCompanyResult, CreateCompanyResponse>();

    }
}
