using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class StockItemsService : BaseService
{
    public StockItemsService(ApiClient client) : base(client, "stockitems")
    {
    }

    public async Task<List<StockItemDto>> GetStockItemsForProductAsync(string id)
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"{_path}/by-product/{id}");

        return await _client.DeserializeResponse<List<StockItemDto>>(response);
    }

    public async Task<StockItemDto> GetStockItemById(string id)
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"{_path}/{id}");

        return await _client.DeserializeResponse<StockItemDto>(response);
    }

    public async Task<ResultCreated<string>> AddNewStockItem(object command)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}/add", command);

        return await _client.DeserializeResponse<ResultCreated<string>>(response);
    }
}
