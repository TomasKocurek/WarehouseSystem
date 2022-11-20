using AutoMapper;
using Domain.Entities;
using Shared.Dto;

namespace Shared.Mapping;
public class MovementProfile : Profile
{
	public MovementProfile()
	{
		CreateMap<Movement, MovementDto>();
	}
}
