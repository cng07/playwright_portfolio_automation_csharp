using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class AboutTests : PortfolioTestBase
{
    private AboutPage _aboutPage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _aboutPage = new AboutPage(Page);
    }

    [Test]
    public async Task VerifyAboutPageUiViaNavigation()
    {
        await GoToHomePageAsync();
        await Page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = "About" }).ClickAsync();
        await Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.DOMContentLoaded);
        await WaitForPathAsync("/about");
        await ExpectTitleAsync("Carlos Ng | About");
        await VerifyAllAboutPageElementsAsync();
    }

    [Test]
    public async Task VerifyAboutPageUiViaDirectUrl()
    {
        await GoToDirectPathAsync("/about", "Carlos Ng | About");
        await VerifyAllAboutPageElementsAsync();
    }

    [Test]
    public async Task VerifyAboutPageApiLinks()
    {
        await GoToDirectPathAsync("/about", "Carlos Ng | About");
        await VerifyInternalPathsApiResponsesAsync("/about", "/privacy", "/terms");
    }

    private async Task VerifyAllAboutPageElementsAsync()
    {
        await _aboutPage.VerifyAccessibilityElementsAsync();
        await _aboutPage.VerifyHeaderAsync();
        await _aboutPage.VerifyProfileSectionAsync();
        await _aboutPage.VerifyHighlightsAndPhilosophyAsync();
        await _aboutPage.VerifyContactLinksAsync();
        await _aboutPage.VerifyFooterSectionAsync();
    }
}
