using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ProjectsPage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Page Header
    private ILocator ProjectsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Projects", Level = 1 });
    private ILocator ProjectTitles => _page.Locator("h3");
    private ILocator ProjectStatusActive => _page.GetByText("Active", new() { Exact = true });
    private ILocator TextHighlights => _page.GetByText("Highlights");
    private ILocator TextTechnologies => _page.GetByText("Technologies");

    // Project 1 Elements
    private ILocator Project1Title => _page.GetByRole(AriaRole.Heading, new() { Name = "Portfolio Website Automation (JavaScript)", Level = 3 });
    private ILocator Project1Card => _page.Locator("div.glass:has(h3:has-text('Portfolio Website Automation (JavaScript)'))");

    // Project 2 Elements
    private ILocator Project2Title => _page.GetByRole(AriaRole.Heading, new() { Name = "Portfolio Website Automation (TypeScript)", Level = 3 });
    private ILocator Project2Card => _page.Locator("div.glass:has(h3:has-text('Portfolio Website Automation (TypeScript)'))");

    // Project 3 Elements
    private ILocator Project3Title => _page.GetByRole(AriaRole.Heading, new() { Name = "Portfolio Website Automation (Python)", Level = 3 });
    private ILocator Project3Card => _page.Locator("div.glass:has(h3:has-text('Portfolio Website Automation (Python)'))");

    // Project 4 Elements
    private ILocator Project4Title => _page.GetByRole(AriaRole.Heading, new() { Name = "QA Practice Framework", Level = 3 });
    private ILocator Project4Card => _page.Locator("div.glass:has(h3:has-text('QA Practice Framework'))");

    // Repository Links
    private ILocator RepositoryLinks => _page.GetByRole(AriaRole.Link, new() { Name = "Repository" });

    // More Projects Section
    private ILocator MoreProjectsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "More Projects Coming", Level = 2 });
    private ILocator MoreProjectsText => _page.GetByText("Check back soon for updates!", new() { Exact = true });

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public ProjectsPage(IPage page)
    {
        _page = page;
    }

    public async Task VerifyAccessibilityElementsAsync()
    {
        var skipToContentHref = await SkipToContentLink.First.GetAttributeAsync("href");
        Assert.That(skipToContentHref, Is.EqualTo("#main-content"));
        await ExpectVisibleAsync(MainContent);
    }

    public async Task VerifyProjectsPageHeaderAsync()
    {
        await ExpectVisibleAsync(ProjectsHeading);
        Assert.That(await ProjectTitles.CountAsync(), Is.EqualTo(4));
        Assert.That(await ProjectStatusActive.CountAsync(), Is.EqualTo(4));
        Assert.That(await TextHighlights.CountAsync(), Is.EqualTo(4));
        Assert.That(await TextTechnologies.CountAsync(), Is.EqualTo(4));
    }

    public async Task VerifyProject1Async()
    {
        await ExpectVisibleAsync(Project1Title);
        await ExpectVisibleAsync(Project1Card.GetByText("Comprehensive Playwright automation test suite for this portfolio website."));
        await ExpectVisibleAsync(Project1Card.GetByText("Cross-browser testing (Chrome, Firefox, Safari, Edge)"));
        await ExpectVisibleAsync(Project1Card.GetByText("Link integrity checks"));
        await ExpectVisibleAsync(Project1Card.GetByText("CI/CD integration with GitHub Actions"));
        await ExpectVisibleAsync(Project1Card.GetByText("JavaScript", new() { Exact = true }));
        await ExpectVisibleAsync(Project1Card.GetByText("GitHub Actions", new() { Exact = true }));
    }

    public async Task VerifyProject2Async()
    {
        await ExpectVisibleAsync(Project2Title);
        await ExpectVisibleAsync(Project2Card.GetByText("Advanced Playwright automation framework using TypeScript with containerization and CI/CD support."));
        await ExpectVisibleAsync(Project2Card.GetByText("Strongly typed test architecture with TypeScript"));
        await ExpectVisibleAsync(Project2Card.GetByText("Page Object Model (POM) implementation"));
        await ExpectVisibleAsync(Project2Card.GetByText("Docker containerization for consistent test environments"));
        await ExpectVisibleAsync(Project2Card.GetByText("Multi-platform CI/CD support (Jenkins & GitHub Actions)"));
        await ExpectVisibleAsync(Project2Card.GetByText("TypeScript", new() { Exact = true }));
        await ExpectVisibleAsync(Project2Card.GetByText("Jenkins", new() { Exact = true }));
        await ExpectVisibleAsync(Project2Card.GetByText("Docker", new() { Exact = true }));
    }

    public async Task VerifyProject3Async()
    {
        await ExpectVisibleAsync(Project3Title);
        await ExpectVisibleAsync(Project3Card.GetByText("UI automation for this portfolio site using Playwright and pytest."));
        await ExpectVisibleAsync(Project3Card.GetByText("Page Object Model (POM) for Home & Projects pages"));
        await ExpectVisibleAsync(Project3Card.GetByText("pytest + playwright integration"));
        await ExpectVisibleAsync(Project3Card.GetByText("HTML report generation via pytest-html"));
        await ExpectVisibleAsync(Project3Card.GetByText("Python", new() { Exact = true }));
        await ExpectVisibleAsync(Project3Card.GetByText("pytest", new() { Exact = true }));
    }

    public async Task VerifyProject4Async()
    {
        await ExpectVisibleAsync(Project4Title);
        await ExpectVisibleAsync(Project4Card.GetByText("Automated end-to-end test suites written in TypeScript using Playwright."));
        await ExpectVisibleAsync(Project4Card.GetByText("Data-driven testing via CSV integration"));
        await ExpectVisibleAsync(Project4Card.GetByText("Page Object Model (POM) architecture"));
        await ExpectVisibleAsync(Project4Card.GetByText("Automated form validation & edge case handling"));
    }

    public async Task VerifyRepositoryLinksAsync()
    {
        var expectedLinks = new[]
        {
            "https://github.com/cng07/playwright_portfolio_automation_javascript",
            "https://github.com/cng07/playwright_portfolio_automation_typescript",
            "https://github.com/cng07/playwright_portfolio_automation_python",
            "https://github.com/cng07/qaPractice"
        };

        Assert.That(await RepositoryLinks.CountAsync(), Is.EqualTo(4));

        var actualLinks = new List<string?>();
        for (var i = 0; i < expectedLinks.Length; i++)
        {
            actualLinks.Add(await RepositoryLinks.Nth(i).GetAttributeAsync("href"));
        }

        Assert.That(actualLinks, Is.EqualTo(expectedLinks));
    }

    public async Task VerifyRepositoryLinksApiResponsesAsync(IAPIRequestContext api)
    {
        var urls = new List<string>();
        var count = await RepositoryLinks.CountAsync();
        for (var i = 0; i < count; i++)
        {
            var href = await RepositoryLinks.Nth(i).GetAttributeAsync("href");
            if (!string.IsNullOrWhiteSpace(href))
            {
                urls.Add(href);
            }
        }

        foreach (var url in urls)
        {
            var response = await api.GetAsync(url, new() { Timeout = 30000 });
            Assert.That(response.Status, Is.GreaterThanOrEqualTo(200), $"Expected repository URL to be reachable: {url}");
            Assert.That(response.Status, Is.LessThan(400), $"Expected repository URL to be reachable: {url}");
        }
    }

    public async Task VerifyMoreProjectsComingSectionAsync()
    {
        await ExpectVisibleAsync(MoreProjectsHeading);
        await ExpectVisibleAsync(MoreProjectsText);
    }

    public async Task VerifyFooterSectionAsync()
    {
        await ExpectVisibleAsync(PrivacyPolicyLink);
        await ExpectVisibleAsync(TermsAndConditionsLink);

        var privacyHref = await PrivacyPolicyLink.First.GetAttributeAsync("href");
        Assert.That(privacyHref, Is.EqualTo("/privacy"));

        var termsHref = await TermsAndConditionsLink.First.GetAttributeAsync("href");
        Assert.That(termsHref, Is.EqualTo("/terms"));
    }

    private static async Task ExpectVisibleAsync(ILocator locator)
    {
        await locator.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
        Assert.That(await locator.First.IsVisibleAsync(), Is.True);
    }
}
