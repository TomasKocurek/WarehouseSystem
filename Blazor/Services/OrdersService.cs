using Blazor.Dtos.Orders;
using Shared;
using Shared.Dto;

namespace Blazor.Services;

public class OrdersService : BaseService
{
    public OrdersService(ApiClient client) : base(client, "orders")
    {

    }

    public async Task<List<OrderListDto>?> GetOrderList()
    {
        var response = await _client.SendAsync(HttpMethod.Get, $"{_path}");

        return await _client.DeserializeResponse<List<OrderListDto>>(response);
    }

    public async Task<ResultCreated<string>?> CreateOrder(CreateOrderDto data)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}", data);

        return await _client.DeserializeResponse<ResultCreated<string>>(response);
    }
}
