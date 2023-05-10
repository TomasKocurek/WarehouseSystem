using API.Movements.Commands.Dispatch;
using API.Movements.Commands.ReceiptCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MovementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("receipt")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Receipt([FromBody] ReceiptCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("dispatch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<DispatchResultDto>> Dispatch([FromBody] DispatchCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
