using Newtonsoft.Json;

namespace Blazor.Services;

public class ApiClient
{
    //todo dependency injection
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7231/api/")
        };
    }

    public Task<HttpResponseMessage> SendAsync(HttpMethod method, string uri, object? content = null)
    {
        HttpRequestMessage request = new(method, new Uri(uri, UriKind.Relative));

        if (content is not null) request.Content = JsonContent.Create(content);

        return _httpClient.SendAsync(request);
    }

    public async Task<T?> DeserializeResponse<T>(HttpResponseMessage message)
    {
        var json = await message.Content.ReadAsStringAsync();

        if (json is not null)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            return default;
        }
    }
}
