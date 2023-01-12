using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Stocks;

public partial class StocksList : ComponentBase
{
    [Inject]
    protected IHxMessageBoxService MessageBox { get; set; } = null!;

    private void OpenNewStock()
    {
        _navManager.NavigateTo("stocks/new");
    }

    private async Task<GridDataProviderResult<StockDto>> GetGridData(GridDataProviderRequest<StockDto> request)
    {
        var stocks = await _stocksService.GetAllStocks();
        return request.ApplyTo(stocks);
    }

    private async Task DeleteStock(StockDto item)
    {
        var result = await MessageBox.ConfirmAsync(
            "Delete product",
            $"Do you really want to delete stock {item.Name}?");

        if (result)
        {
            await _stocksService.DeleteStock(item.Id.ToString());
            _navManager.NavigateTo(_navManager.Uri, true);
        }
    }
}
