using Microsoft.AspNetCore.Components;
using Shared.Dto;

namespace Blazor.Pages;

public partial class NewProduct : ComponentBase
{
    private ProductDto newProduct = new();

    //todo validace
    private async Task AddProductAsync()
    {
        var command = new
        {
            newProduct.Name
        };

        HttpContent content = JsonContent.Create(command);

        await _productsService.CreateNewProduct(content);

        _navigationManager.NavigateTo("catalog");
    }
}
