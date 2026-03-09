using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class ResumeTests : PortfolioTestBase
{
    private ResumePage _resumePage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _resumePage = new ResumePage(Page, BaseUrl);
    }

    [Test]
    public async Task VerifyResumePageUiViaNavigation()
    {
        await GoToHomePageAsync();
        await Page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = "Resume" }).ClickAsync();
        await Page.WaitForURLAsync("**/resume");
        await Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.Load);
        await ExpectVisibleAsync(Page.GetByRole(Microsoft.Playwright.AriaRole.Heading, new() { Name = "Resume", Level = 1 }));
        await Page.WaitForFunctionAsync("expectedTitle => document.title === expectedTitle", "Carlos Ng | Resume");
        await ExpectTitleAsync("Carlos Ng | Resume");
        await VerifyAllResumePageElementsAsync();
    }

    [Test]
    public async Task VerifyResumePdfDownload()
    {
        await GoToDirectPathAsync("/resume", "Carlos Ng | Resume");
        await _resumePage.VerifyDownloadPdfButtonAsync();
        await _resumePage.DownloadPdfAndVerifyAsync(Api);
    }

    [Test]
    public async Task VerifyResumePageApiLinks()
    {
        await GoToDirectPathAsync("/resume", "Carlos Ng | Resume");
        await _resumePage.VerifyResumePdfApiResponseAsync(Api);
        await VerifyInternalPathsApiResponsesAsync("/resume", "/privacy", "/terms");
    }

    private async Task VerifyAllResumePageElementsAsync()
    {
        await _resumePage.VerifyAccessibilityElementsAsync();
        await _resumePage.VerifyResumeHeaderAsync();
        await _resumePage.VerifyDownloadPdfButtonAsync();
        await _resumePage.VerifyResumeViewerSectionAsync();
        await _resumePage.VerifyFooterSectionAsync();
    }
}
