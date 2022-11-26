using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Catalog;

public partial class ProductDetail : ComponentBase
{
    [Parameter]
    public string? Id { get; set; }

    private ProductDto product = new();

    //components
    private bool SaveBtnEnabled { get; set; } = true;
    private bool StocksHidden { get; set; } = true;

    protected override async Task<Task> OnInitializedAsync()
    {
        if (Id != null && Id != "new") await LoadProductAsync();

        return base.OnInitializedAsync();
    }

    private async Task LoadProductAsync()
    {
        product = await _productsService.GetProductById(Id!);
        SaveBtnEnabled = false;
        StocksHidden = false;
    }

    //todo validace
    //todo create vs update
    private async Task SaveProductAsync()
    {
        var command = new
        {
            product.Name
        };

        await _productsService.CreateNewProduct(command);

        _navigationManager.NavigateTo("catalog");
    }

    private async Task<GridDataProviderResult<StockItemDto>> GetGridData(GridDataProviderRequest<StockItemDto> request)
    {
        if (!StocksHidden)
        {
            var stockItem = await _stockItemService.GetStockItemsForProductAsync(Id!);
            return request.ApplyTo(stockItem);
        }
        else
        {
            return request.ApplyTo(new List<StockItemDto>());
        }
    }

    private void OpenNewStockItem()
    {
        _navigationManager.NavigateTo($"warehouse/{Id}/new");
    }
}
