using Bunit;
using Xunit;

namespace EndToEndTests.Steps;

[Binding]
public class HelloWorldSteps : IDisposable
{
    private readonly TestContext _testContext;
    private IRenderedComponent<HelloWorld.Blazor.Pages.Index>? _component;

    public HelloWorldSteps()
    {
        _testContext = new TestContext();
    }
    [Given(@"I have navigated to the Blazor page")]
    public void GivenIHaveNavigatedToTheBlazorPage()
    {
        _component = _testContext.RenderComponent<HelloWorld.Blazor.Pages.Index>();
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