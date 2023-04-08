using API.Orders.Commands.Create;
using API.Orders.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrderListDto>>> GetAllOrders()
    {
        return Ok(await _mediator.Send(new GetAllOrdersQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ResultCreated<string>>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
