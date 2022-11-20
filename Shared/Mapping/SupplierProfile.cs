using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;
public class SupplierProfile : Profile
{
	public SupplierProfile()
	{
		CreateMap<Supplier, SupplierDto>();
	}
}
