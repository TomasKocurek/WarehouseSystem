namespace Blazor.Services;

public class BaseService
{
    protected readonly ApiClient _client;
    protected readonly string _path;

    public BaseService(ApiClient client, string path)
    {
        _client = client;
        _path = path;
    }
}
