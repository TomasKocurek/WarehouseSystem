using Microsoft.AspNetCore.Components;
using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Pages.Warehouse;

public partial class StockItemDetail : ComponentBase
{
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? ProductId { get; set; }

    private StockItemDto stockItem = new();
    private List<StockDto> stocks = new();
    private FormModel model = new();

    //components
    private bool SaveBtnEnabled { get; set; } = true;

    protected override async Task<Task> OnInitializedAsync()
    {
        if (Id != null && Id != "new") await LoadStockItem();

        stocks = await _stocksService.GetAllStocks();

        return base.OnInitializedAsync();
    }

    private async Task LoadStockItem()
    {
        stockItem = await _stockItemService.GetStockItemById(Id!);
        model = new(stockItem.Stock.Id.ToString(), stockItem.BarCode, stockItem.Amount);
        ProductId = stockItem.Product!.Id.ToString();
        SaveBtnEnabled = false;
    }

    //todo create vs update
    private async Task SaveStockItem()
    {
        //todo
        var command = new
        {
            model.BarCode,
            model.StockId,
            ProductId,
            model.Amount
        };

        var result = await _stockItemService.AddNewStockItem(command);

        if (result != null)
        {
            _navigationManager.NavigateTo($"warehouse/{result.Id}", true);
        }
    }
}

internal class FormModel
{
    [Required(ErrorMessage = "Choose stock")]
    public string StockId { get; set; }

    [Required(ErrorMessage = "Enter barcode")]
    public string BarCode { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public int Amount { get; set; }

    public FormModel() { }

    public FormModel(string stockId, string barCode, int amount)
    {
        StockId = stockId;
        BarCode = barCode;
        Amount = amount;
    }
}