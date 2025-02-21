using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CraeteProductProfile : Profile
{
    public CraeteProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResult>();
    }
}
