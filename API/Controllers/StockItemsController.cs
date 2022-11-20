using API.StockItems.Commands;
using API.StockItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockItemsController : ControllerBase
{
    //todo dto
    private readonly IMediator _mediator;

    public StockItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResultCreated<string>>> AddNewStockItem([FromBody] AddNewStockItemCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StockItemDto?>> GetStockItemById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new GetStockItemByIdQuery(id)));
    }

    [HttpDelete("{id}/delete")]
    public async Task<ActionResult> DeleteStockItemById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new DeleteStockItemByIdCommand(id)));
    }

    [HttpPost("{id}/add-movement")]
    public async Task<ActionResult> AddMovementToStockItem([FromRoute] string id, [FromBody] AddMovementCommand command)
    {
        command.Id = id;
        return Ok(await _mediator.Send(command));
    }
}
