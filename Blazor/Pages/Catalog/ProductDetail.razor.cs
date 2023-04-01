using Domain.Entities;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Pages.Catalog;

public partial class ProductDetail : ComponentBase
{
    [Parameter]
    public string? Id { get; set; }

    private ProductDto product = new();

    //components
    private bool SaveBtnEnabled { get; set; } = true;
    private bool StocksHidden { get; set; } = true;

    private FormModel model = new();

    protected override async Task<Task> OnInitializedAsync()
    {
        if (Id != null && Id != "new") await LoadProductAsync();

        return base.OnInitializedAsync();
    }

    private async Task LoadProductAsync()
    {
        product = await _productsService.GetProductById(Id!);
        model = new(product.Name)
        {
            Price = product.Price.Amount,
            Height = product.PackageSize.Height,
            Depth = product.PackageSize.Depth,
            Width = product.PackageSize.Width,
        };
        SaveBtnEnabled = false;
        StocksHidden = false;
    }

    //todo validace
    //todo create vs update
    private async Task SaveProduct()
    {
        var command = new
        {
            model.Name,
            Price = new Money(model.Price),
            PackageSize = new Size(model.Height, model.Width, model.Depth)
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

internal class FormModel
{
    [Required(ErrorMessage = "Enter name")]
    public string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public int Height { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public int Width { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Enter number bigger then 0")]
    public int Depth { get; set; }

    public FormModel() { }

    public FormModel(string name)
    {
        Name = name;
    }
}