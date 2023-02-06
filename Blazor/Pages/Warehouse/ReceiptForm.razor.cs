using Blazor.Pages.Components;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Pages.Warehouse;

public partial class ReceiptForm : ComponentBase
{
    //Components
    private FormModel model = new();
    private HxGrid<ReceiptItem>? grid;
    private ProductSearchBar? ProductSearchBar;

    //Data
    private List<ReceiptItem> itemsToReceipt = new();
    private ReceiptItem selectedItem = new();
    private List<StockDto> Stocks = new();

    protected override async Task OnInitializedAsync()
    {
        Stocks = await _stocksService.GetAllStocks();
    }

    private async Task ReceiptItems()
    {
        if (itemsToReceipt.Any())
        {
            model.ReceiptItems = itemsToReceipt;
            await _movementsService.Receipt(model);
        }

        _navigationManager.NavigateTo("catalog");
    }

    private async Task<GridDataProviderResult<ReceiptItem>> GetGridData(GridDataProviderRequest<ReceiptItem> request)
    {
        return request.ApplyTo(itemsToReceipt);
    }

    private void HandleChange(ReceiptItem newReceiptItem)
    {
        selectedItem = newReceiptItem;
    }

    private void ReceiptNext()
    {
        if (ProductSearchBar?.selectedProduct != null)
        {
            itemsToReceipt.Add(new()
            {
                Name = ProductSearchBar!.selectedProduct.Name,
                ProductId = ProductSearchBar!.selectedProduct.Id
            });
            grid?.RefreshDataAsync();
        }
    }

    private class FormModel
    {
        [Required(ErrorMessage = "Enter invoice name")]
        public string Invoice { get; set; }

        [Required(ErrorMessage = "Enter invoice data")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Select stock")]
        public string StockId { get; set; }

        public List<ReceiptItem> ReceiptItems { get; set; } = new();
    }
}

internal class ReceiptItem
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
}