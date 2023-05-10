using Blazor.Dtos.Movements;
using Shared.Dto;

namespace Blazor.Services;

public class MovementsService : BaseService
{
    public MovementsService(ApiClient client) : base(client, "movements") { }

    public async Task Receipt(object receiptCommand)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}/receipt", receiptCommand);
    }

    public async Task<DispatchResultDto?> Dispatch(DispatchDto data)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}/dispatch", data);

        return await _client.DeserializeResponse<DispatchResultDto>(response);
    }
}
