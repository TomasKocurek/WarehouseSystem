using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class StockItemsService
{
    private readonly ApiClient _client;

    public StockItemsService(ApiClient client)
    {
        _client = client;
    }

    public async Task<List<StockItemDto>> GetStockItemsForProductAsync(string id)
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"stockitems/by-product/{id}");

        return await _client.DeserializeResponse<List<StockItemDto>>(response);
    }

    public async Task<StockItemDto> GetStockItemById(string id)
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"stockitems/{id}");

        return await _client.DeserializeResponse<StockItemDto>(response);
    }

    public async Task<ResultCreated<string>> AddNewStockItem(object command)
    {
        var response = await _client.SendAsync(HttpMethod.Post, "stockitems/add", command);

        return await _client.DeserializeResponse<ResultCreated<string>>(response);
    }
}
