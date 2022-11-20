using API.Products.Commands;
using API.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        return Ok(await _mediator.Send(new GetAllProductsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> GetProductById([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResultCreated<string>>> AddNewProduct([FromBody] AddNewProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] string id)
    {
        return Ok(await _mediator.Send(new DeleteProductCommand(id)));
    }
}
