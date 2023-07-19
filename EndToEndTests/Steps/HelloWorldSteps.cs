using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EndToEndTests.Steps;

[Binding]
public class HelloWorldSteps : IDisposable
{
    private readonly TestContext _testContext;
    private IRenderedComponent<HelloWorld.Blazor.Pages.Index>? _component;

    public HelloWorldSteps()
    {
        var httpClient = new HttpClient();
        _testContext = new TestContext();
        _testContext.Services.AddScoped(_ => httpClient);
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

    public void Dispose()
    {
        _testContext.Dispose();
    }
}