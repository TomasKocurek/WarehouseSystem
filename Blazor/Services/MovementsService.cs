namespace Blazor.Services;

public class MovementsService : BaseService
{
    public MovementsService(ApiClient client) : base(client, "movements") { }

    public async Task Receipt(object receiptCommand)
    {
        var response = await _client.SendAsync(HttpMethod.Post, $"{_path}/receipt", receiptCommand);
    }
}
