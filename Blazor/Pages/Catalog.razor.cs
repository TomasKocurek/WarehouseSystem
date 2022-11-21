using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages;

public partial class Catalog : ComponentBase
{
    private async Task<GridDataProviderResult<ProductDto>> GetGridData(GridDataProviderRequest<ProductDto> request)
    {
        var products = await _productsService.GetAllProductsAsync();

        return request.ApplyTo(products);
    }

    private void NavigateToNewProduct()
    {
        _navManager.NavigateTo("catalog/new");
    }
}