using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class ProductsService
{
    private readonly ApiClient _apiClient;

    public ProductsService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var response = await _apiClient.SendAsync(HttpMethod.Get, "products/all");

        return await _apiClient.DeserializeResponse<List<ProductDto>>(response);
    }

    public async Task<ResultCreated<string>> CreateNewProduct(HttpContent newProduct)
    {
        var response = await _apiClient.SendAsync(HttpMethod.Post, "products/add", newProduct);

        return await _apiClient.DeserializeResponse<ResultCreated<string>>(response);
    }
}
