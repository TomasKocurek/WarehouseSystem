using Blazor.Dtos.Orders;
using Blazor.Pages.Components;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Orders;

public partial class OrderForm : ComponentBase
{
    //Components
    private ProductSearchBar? productSearchBar;
    private HxGrid<OrderItemListDto>? grid;

    //State
    private CreateOrderDto model = new();
    private List<OrderItemListDto> orderItems = new();
    private OrderItemListDto? selectedItem;

    private async Task CreateOrderAsync()
    {
        model.Items = orderItems.ConvertAll(i => new OrderItemDto() { Amount = i.Amount, ProductId = i.ProductId });
        await _ordersService.CreateOrder(model);

        _navNamager.NavigateTo("orders", true);
    }

    private void AddItem()
    {
        if (productSearchBar?.selectedProduct != null)
        {
            orderItems.Add(new()
            {
                ProductName = productSearchBar!.selectedProduct.Name,
                ProductId = productSearchBar!.selectedProduct.Id
            });
            grid?.RefreshDataAsync();
        }
    }

    private void HandleGridSelectionChange(OrderItemListDto newSelectedItem)
    {
        selectedItem = newSelectedItem;
    }

    private async Task<GridDataProviderResult<OrderItemListDto>> GetGridData(GridDataProviderRequest<OrderItemListDto> request)
    {
        return request.ApplyTo(orderItems);
    }
}

internal class OrderItemListDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Amount { get; set; }
}
