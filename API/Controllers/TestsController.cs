using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestsController
{
    private readonly WarehouseDbContext _context;

    public TestsController(WarehouseDbContext context)
    {
        _context = context;
    }

    [HttpDelete("clear")]
    public async Task ClearDatabase()
    {
        await _context.DeleteRangeAsync<Movement>();
        await _context.DeleteRangeAsync<StockItem>();
        await _context.DeleteRangeAsync<Stock>();
        await _context.DeleteRangeAsync<Supplier>();
        await _context.DeleteRangeAsync<Product>();

        await _context.SaveChangesAsync();
    }
}
