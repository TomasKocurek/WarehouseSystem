using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages.Catalog;

public partial class Catalog : ComponentBase
{
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
}