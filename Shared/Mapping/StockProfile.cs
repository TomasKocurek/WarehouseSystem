using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;
public class StockProfile : Profile
{
	public StockProfile()
	{
		CreateMap<Stock, StockDto>();
	}
}
