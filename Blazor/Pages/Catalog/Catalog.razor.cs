using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Catalog;

public partial class Catalog : ComponentBase
{
    [Inject]
    protected IHxMessageBoxService MessageBox { get; set; } = null!;

    private async Task<GridDataProviderResult<ProductDto>> GetGridData(GridDataProviderRequest<ProductDto> request)
    {
        var products = await _productsService.GetAllProductsAsync();

        return request.ApplyTo(products);
    }

    private void OpenNewProduct()
    {
        _navManager.NavigateTo("catalog/new");
    }

    private void OpenProduct(ProductDto item)
    {
        _navManager.NavigateTo($"catalog/{item.Id}");
    }

    private async Task DeleteProduct(ProductDto item)
    {
        var result = await MessageBox.ConfirmAsync(
            "Delete product",
            $"Do you really want to delete product {item.Name}? All of his stock items will be also deleted.");

        if (result)
        {
            await _productsService.DeleteProduct(item.Id.ToString());
            _navManager.NavigateTo(_navManager.Uri, true);
        }
    }
}