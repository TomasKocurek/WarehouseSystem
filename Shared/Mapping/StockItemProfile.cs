using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;
public class StockItemProfile : Profile
{
	public StockItemProfile()
	{
		CreateMap<StockItem, StockItemDto>();
	}
}
