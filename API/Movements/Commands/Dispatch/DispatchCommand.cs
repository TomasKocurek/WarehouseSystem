using MediatR;
using Shared.Dto;

namespace API.Movements.Commands.Dispatch;

public class DispatchCommand : IRequest<DispatchResultDto>
{
    public List<string> Orders { get; set; } = new();
}

