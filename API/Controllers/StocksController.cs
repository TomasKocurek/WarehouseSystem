using API.Stocks.Commands;
using API.Stocks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto;

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

    [HttpPost]
    public async Task<ActionResult<ResultCreated<string>>> CreateNewStock([FromBody] CreateNewStockCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StockDto?>> GetStockById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new GetStockByIdQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult<List<StockDto>>> GetAllStocks()
    {
        return Ok(await _mediator.Send(new GetAllStocksQuery()));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStockById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new DeleteStockByIdCommand(id)));
    }

    [HttpGet("suggest-stock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<StockDto?>> SuggestStock([FromBody] SuggestStockQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
