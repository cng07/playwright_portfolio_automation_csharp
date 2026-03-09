using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class ContactTests : PortfolioTestBase
{
    private ContactPage _contactPage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _contactPage = new ContactPage(Page);
    }

    [Test]
    public async Task VerifyContactPageUiViaNavigation()
    {
        await GoToHomePageAsync();
        await Page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = "Contact" }).ClickAsync();
        await Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.DOMContentLoaded);
        await WaitForPathAsync("/contact");
        await ExpectTitleAsync("Carlos Ng | Contact");
        await VerifyAllContactPageElementsAsync();
    }

    [Test]
    public async Task VerifyContactPageUiViaDirectUrl()
    {
        await GoToDirectPathAsync("/contact", "Carlos Ng | Contact");
        await VerifyAllContactPageElementsAsync();
    }

    [Test]
    public async Task VerifyContactPageApiLinks()
    {
        await GoToDirectPathAsync("/contact", "Carlos Ng | Contact");

        await VerifyInternalPathsApiResponsesAsync("/contact", "/privacy", "/terms");
        await VerifyUrlsApiResponsesAsync(
            [
                "https://www.linkedin.com/in/carlosng07",
                "https://github.com/cng07",
                "https://ieeexplore.ieee.org/author/37086553247",
                "https://atsqa.org/certified-testers/profile/6676da6cab1b424aa4070395ff71f490"
            ],
            timeoutMs: 30000,
            urlType: "external URL");
    }

    private async Task VerifyAllContactPageElementsAsync()
    {
        await _contactPage.VerifyAccessibilityElementsAsync();
        await _contactPage.VerifyHeaderAsync();
        await _contactPage.VerifyContactMethodsSectionAsync();
        await _contactPage.VerifyFooterSectionAsync();
    }
}
