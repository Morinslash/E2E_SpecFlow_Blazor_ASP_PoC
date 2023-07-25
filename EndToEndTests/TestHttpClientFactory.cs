namespace EndToEndTests;

public class TestHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClient _inMemoryClient;

    public TestHttpClientFactory(HttpClient inMemoryClient)
    {
        _inMemoryClient = inMemoryClient;
    }

    public HttpClient CreateClient(string name)
    {
        return _inMemoryClient;
    }
}