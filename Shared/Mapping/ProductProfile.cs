using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}
