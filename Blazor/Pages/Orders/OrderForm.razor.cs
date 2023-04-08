using Blazor.Dtos.Orders;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Orders;

public partial class OrderForm : ComponentBase
{
    private CreateOrderDto model = new();

    private async Task CreateOrderAsync()
    {
        await _ordersService.CreateOrder(model);

        _navNamager.NavigateTo("orders", true);
    }
}
