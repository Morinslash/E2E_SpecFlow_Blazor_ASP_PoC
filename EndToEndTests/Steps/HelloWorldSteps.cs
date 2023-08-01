using Bunit;
using HelloWorld.API.DbSeeders;
using HelloWorld.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EndToEndTests.Steps;

[Binding]
public class HelloWorldSteps : IClassFixture<HelloWorldApiFactory>, IDisposable
{
    private readonly TestContext _testContext;
    private IRenderedComponent<HelloWorld.Blazor.Pages.Index>? _component;
    private readonly HelloWorldApiFactory _factory;
    private readonly MongoSeeder _seeder;

    public HelloWorldSteps(HelloWorldApiFactory factory)
    {
        _factory = factory;
        
        using var scope = _factory.Services.CreateScope();
        _seeder = scope.ServiceProvider.GetRequiredService<MongoSeeder>();
        
        var client = _factory.CreateClient();
        var testHttpClientFactory = new TestHttpClientFactory(client);
        IBackendApiClient backendService = new BackendApiClient(testHttpClientFactory);

        _testContext = new TestContext();
        _testContext.Services.AddScoped(_ => backendService);
    }

    [BeforeScenario()]
    public async Task SeedDatabase()
    {
        await _seeder.SeedAsync();
    }

    [AfterScenario()]
    public async Task DropDatabase()
    {
        await _seeder.DropDatabaseAsync();
    }

    [Given(@"I have navigated to the Blazor page")]
    public void GivenIHaveNavigatedToTheBlazorPage()
    {
        _component = _testContext.RenderComponent<HelloWorld.Blazor.Pages.Index>();
        _component.WaitForState(() => _component.Find("h1").TextContent == "Hello World");
    }

    [Then(@"I should see the '(.*)' message")]
    public void ThenIShouldSeeTheMessage(string expectedInput)
    {
        Assert.Contains(expectedInput, _component.Markup);
    }

    public async void Dispose()
    {
        _testContext.Dispose();
        await _factory.DisposeAsync();
    }
}