using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class ExperienceTests : PortfolioTestBase
{
    private ExperiencePage _experiencePage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _experiencePage = new ExperiencePage(Page);
    }

    [Test]
    public async Task VerifyExperiencePageUiViaNavigation()
    {
        await GoToHomePageAsync();
        await Page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = "Experience" }).ClickAsync();
        await Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.DOMContentLoaded);
        await WaitForPathAsync("/experience");
        await ExpectTitleAsync("Carlos Ng | Experience");
        await VerifyAllExperiencePageElementsAsync();
    }

    [Test]
    public async Task VerifyExperiencePageUiViaDirectUrl()
    {
        await GoToDirectPathAsync("/experience", "Carlos Ng | Experience");
        await VerifyAllExperiencePageElementsAsync();
    }

    [Test]
    public async Task VerifyExperiencePageApiLinks()
    {
        await GoToDirectPathAsync("/experience", "Carlos Ng | Experience");
        await VerifyInternalPathsApiResponsesAsync("/experience", "/privacy", "/terms");

        foreach (var url in new[]
                 {
                     "https://datacom.com/nz/en",
                     "https://www.planit.com/",
                     "https://dxc.com/",
                     "https://www.davi.com.ph/",
                     "https://www.accenture.com/ph-en"
                 })
        {
            var response = await Api.GetAsync(url, new() { Timeout = 30000 });
            Assert.That(response.Status, Is.GreaterThanOrEqualTo(200), $"Expected external URL to be reachable: {url}");
            Assert.That(response.Status, Is.LessThan(500), $"Expected external URL to be reachable: {url}");
            Assert.That(response.Status, Is.Not.EqualTo(404), $"Expected external URL not to be missing: {url}");
        }
    }

    private async Task VerifyAllExperiencePageElementsAsync()
    {
        await _experiencePage.VerifyAccessibilityElementsAsync();
        await _experiencePage.VerifyPageHeaderAsync();
        await _experiencePage.VerifyExperienceEntriesAsync();
        await _experiencePage.VerifyCompanyLinksAsync();
        await _experiencePage.VerifyFooterSectionAsync();
    }
}
