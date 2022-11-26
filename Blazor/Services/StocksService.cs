using Shared.Dto;

namespace Blazor.Services;

public class StocksService
{
    private readonly ApiClient _client;

    public StocksService(ApiClient client)
    {
        _client = client;
    }

    public async Task<List<StockDto>> GetAllStocks()
    {
        var result = await _client.SendAsync(HttpMethod.Get, "stocks/all");
        return await _client.DeserializeResponse<List<StockDto>>(result);
    }
}
