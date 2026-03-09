using PlaywrightTests.Framework;
using PlaywrightTests.Pages;
using Microsoft.Playwright;

namespace PlaywrightTests.Tests;

[TestFixture]
public class EducationTests : PortfolioTestBase
{
    private EducationPage _educationPage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _educationPage = new EducationPage(Page);
    }

    [Test]
    public async Task VerifyEducationPageUiViaNavigation()
    {
        await GoToHomePageAsync();

        var moreButton = Page.GetByRole(AriaRole.Button, new() { Name = "More" });
        await ExpectVisibleAsync(moreButton);
        await moreButton.ClickAsync();

        var educationMenuItem = Page.GetByRole(AriaRole.Menuitem, new() { Name = "Education" });
        await ExpectVisibleAsync(educationMenuItem);
        await educationMenuItem.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await WaitForPathAsync("/education");
        await ExpectTitleAsync("Carlos Ng | Education");
        await VerifyAllEducationPageElementsAsync();
    }

    [Test]
    public async Task VerifyEducationPageUiViaDirectUrl()
    {
        await GoToDirectPathAsync("/education", "Carlos Ng | Education");
        await VerifyAllEducationPageElementsAsync();
    }

    [Test]
    public async Task VerifyEducationPageApiLinks()
    {
        await GoToDirectPathAsync("/education", "Carlos Ng | Education");
        await VerifyInternalPathsApiResponsesAsync("/education", "/privacy", "/terms");
        await VerifyUrlsApiResponsesAsync(
            [
                "https://ieeexplore.ieee.org/xpl/conhome/8555972/proceeding",
                "https://ieeexplore.ieee.org/document/8574836"
            ],
            timeoutMs: 30000,
            urlType: "publication URL");
    }

    private async Task VerifyAllEducationPageElementsAsync()
    {
        await _educationPage.VerifyAccessibilityElementsAsync();
        await _educationPage.VerifyHeaderAsync();
        await _educationPage.VerifyTertiarySectionAsync();
        await _educationPage.VerifyLeadershipSectionAsync();
        await _educationPage.VerifySecondarySectionAsync();
        await _educationPage.VerifyPublicationsSectionAsync();
        await _educationPage.VerifyFooterSectionAsync();
    }
}
