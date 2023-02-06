using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Components;

public partial class ProductSearchBar : ComponentBase
{
    //Data
    List<SearchProduct> products = new();
    public SearchProduct selectedProduct;

    public string Placeholder => selectedProduct?.Name ?? "Search product";

    protected override async Task OnInitializedAsync()
    {
        products = (await _productsService
            .GetAllProductsAsync())
            .ConvertAll(p => new SearchProduct(p.Id.ToString(), p.Name));
    }

    private void OnItemSelected(SearchProduct product)
    {
        selectedProduct = product;
    }

    private async Task<SearchBoxDataProviderResult<SearchProduct>> ProvideSearchResults(SearchBoxDataProviderRequest request)
    {
        return new()
        {
            Data = products.Where(i => i.Name.Contains(request.UserInput, StringComparison.OrdinalIgnoreCase))
        };
    }
}

public class SearchProduct
{
    public string Id { get; set; }
    public string Name { get; set; }

    public SearchProduct(string id, string name)
    {
        Id = id;
        Name = name;
    }
}