using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class ProjectsTests : PortfolioTestBase
{
    private ProjectsPage _projectsPage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _projectsPage = new ProjectsPage(Page);
    }

    [Test]
    public async Task VerifyProjectsPageViaNavigation()
    {
        await GoToHomePageAsync();
        await Page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = "Projects", Exact = true }).ClickAsync();
        await Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.DOMContentLoaded);
        await WaitForPathAsync("/projects");
        await ExpectTitleAsync("Carlos Ng | Projects");

        await _projectsPage.VerifyAccessibilityElementsAsync();
        await _projectsPage.VerifyProjectsPageHeaderAsync();
        await _projectsPage.VerifyProject1Async();
        await _projectsPage.VerifyProject2Async();
        await _projectsPage.VerifyProject3Async();
        await _projectsPage.VerifyProject4Async();
        await _projectsPage.VerifyRepositoryLinksAsync();
        await _projectsPage.VerifyMoreProjectsComingSectionAsync();
        await _projectsPage.VerifyFooterSectionAsync();
    }

    [Test]
    public async Task VerifyProjectsPageUiViaDirectUrl()
    {
        await GoToDirectPathAsync("/projects", "Carlos Ng | Projects");
        await VerifyAllProjectsPageElementsAsync();
    }

    [Test]
    public async Task VerifyProjectsPageApiLinks()
    {
        await GoToDirectPathAsync("/projects", "Carlos Ng | Projects");
        await _projectsPage.VerifyRepositoryLinksAsync();
        await _projectsPage.VerifyRepositoryLinksApiResponsesAsync(Api);
        await VerifyInternalPathsApiResponsesAsync("/projects", "/privacy", "/terms");
    }

    private async Task VerifyAllProjectsPageElementsAsync()
    {
        await _projectsPage.VerifyAccessibilityElementsAsync();
        await _projectsPage.VerifyProjectsPageHeaderAsync();
        await _projectsPage.VerifyProject1Async();
        await _projectsPage.VerifyProject2Async();
        await _projectsPage.VerifyProject3Async();
        await _projectsPage.VerifyProject4Async();
        await _projectsPage.VerifyRepositoryLinksAsync();
        await _projectsPage.VerifyMoreProjectsComingSectionAsync();
        await _projectsPage.VerifyFooterSectionAsync();
    }
}
