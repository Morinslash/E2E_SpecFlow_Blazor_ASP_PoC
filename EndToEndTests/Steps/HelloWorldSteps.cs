using Bunit;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using HelloWorld.API.DbSeeders;
using HelloWorld.Blazor.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Xunit;

namespace EndToEndTests.Steps;

[Binding]
public class HelloWorldSteps : IClassFixture<WebApplicationFactory<HelloWorld.API.Program>>, IDisposable
{
    private readonly TestContext _testContext;
    private IRenderedComponent<HelloWorld.Blazor.Pages.Index>? _component;
    private readonly WebApplicationFactory<HelloWorld.API.Program> _factory;

    readonly IContainer _mongoContainer = new ContainerBuilder()
        .WithImage("mongo:latest")
        .WithPortBinding(27017, 27017)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
        .Build();

    public HelloWorldSteps(WebApplicationFactory<HelloWorld.API.Program> factory)
    {
        _factory = factory;

        

        var client = _factory.CreateClient();
        var testHttpClientFactory = new TestHttpClientFactory(client);
        IBackendApiClient backendService = new BackendApiClient(testHttpClientFactory);

        _testContext = new TestContext();
        _testContext.Services.AddScoped(_ => backendService);
    }

    [BeforeScenario()]
    public void StartTestContainer()
    {
        _mongoContainer.StartAsync().Wait();
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var mongoClient = scopedServices.GetRequiredService<IMongoClient>();
            var configuration = scopedServices.GetRequiredService<IConfiguration>();
            var seeder = new MongoSeeder(mongoClient, configuration);
            seeder.SeedAsync().Wait();
        }
    }

    [AfterScenario()]
    public void StopTestContainer()
    {
        _mongoContainer.StopAsync().Wait();
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