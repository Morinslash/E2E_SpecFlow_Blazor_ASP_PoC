namespace HelloWorld.Blazor.Services;

public interface IBackendApiClient
{
    Task<string> GetHello();
}

public class BackendApiClient : IBackendApiClient
{
    private readonly HttpClient _client;

    public BackendApiClient(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("helloClient");
    }

    public Task<string> GetHello()
    {
        return _client.GetStringAsync("/helloworld");
    }
}