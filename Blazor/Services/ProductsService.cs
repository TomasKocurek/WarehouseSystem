using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class ProductsService : BaseService
{
    public ProductsService(ApiClient client) : base(client, "products")
    {
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"{_path}/all");

        return await _client.DeserializeResponse<List<ProductDto>>(response);
    }

    public async Task<ProductDto> GetProductById(string id)
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"{_path}/{id}");

        return await _client.DeserializeResponse<ProductDto>(response);
    }

    public async Task<ResultCreated<string>> CreateNewProduct(object newProduct)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}/add", newProduct);

        return await _client.DeserializeResponse<ResultCreated<string>>(response);
    }

    public Task DeleteProduct(string id)
    {
        return _client.SendAsync(HttpMethod.Delete, $"{_path}/delete/{id}");
    }
}
