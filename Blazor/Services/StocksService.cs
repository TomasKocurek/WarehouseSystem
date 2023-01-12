using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class StocksService : BaseService
{
    public StocksService(ApiClient client) : base(client, "stocks")
    {
    }

    public async Task<List<StockDto>> GetAllStocks()
    {
        var result = await _client.SendAsync(HttpMethod.Get, _path);
        return await _client.DeserializeResponse<List<StockDto>>(result);
    }

    public async Task<ResultCreated<string>> CreateNewStock(object command)
    {
        var result = await _client.SendAsync(HttpMethod.Post, _path, command);
        return await _client.DeserializeResponse<ResultCreated<string>>(result);
    }

    public Task DeleteStock(string id)
    {
        return _client.SendAsync(HttpMethod.Delete, $"{_path}/{id}");
    }
}
