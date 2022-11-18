using API.StockItems.Queries;
using API.Stocks.Commands;
using API.Stocks.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ResultCreated<string>>> CreateNewStock([FromBody] CreateNewStockCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StockItem?>> GetStockById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new GetStockByIdQuery(id)));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<StockItem>>> GetAllStocks()
    {
        return Ok(await _mediator.Send(new GetAllStocksQuery()));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteStockById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new DeleteStockByIdCommand(id)));
    }
}
