using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Orders;

public partial class OrdersList : ComponentBase
{
    private HashSet<OrderListDto> selectedOrders = new();

    private void OpenNewOrder()
    {
        _navManager.NavigateTo("orders/new");
    }

    private void ShipSelectedOrders()
    {
        //Todo
    }

    private async Task<GridDataProviderResult<OrderListDto>> GetGridData(GridDataProviderRequest<OrderListDto> request)
    {
        var orders = await _orderService.GetOrderList();
        return request.ApplyTo(orders);
    }
}
