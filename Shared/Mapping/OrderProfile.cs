using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderListDto>();
    }
}
