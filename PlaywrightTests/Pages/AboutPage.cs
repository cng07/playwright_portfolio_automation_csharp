using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class AboutPage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Header Elements
    private ILocator AboutMeHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "About Me", Level = 1 });
    private ILocator IntroductionHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "I'm Carlos Ng", Level = 2 });

    // Profile Section
    private ILocator ExperienceText => _page.GetByText("A Test Automation Engineer with 7+ years of experience in software testing");
    private ILocator MainToolText => _page.GetByText("Main tool I use these days: Playwright - TypeScript");
    private ILocator ProfileNameHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Carlos Angelo E. Ng", Level = 3 });
    private ILocator JobTitleText => _page.GetByText("Senior Quality Assurance Automation Engineer");
    private ILocator ProfileImage1 => _page.GetByAltText("Carlos Angelo E. Ng - Professional");
    private ILocator ProfileImage2 => _page.GetByAltText("Carlos Angelo E. Ng - Original");

    // Highlights and Philosophy
    private ILocator FastExecutionText => _page.GetByText("Fast Execution", new() { Exact = true });
    private ILocator FastExecutionDetail => _page.GetByText("Reduced execution time from 19 hours to 4 hours for 300+ test cases.", new() { Exact = true });
    private ILocator MaintainedCodeText => _page.GetByText("Maintained Code", new() { Exact = true });
    private ILocator MaintainedCodeDetail => _page.GetByText("Stable, clean, and reliable test suites.", new() { Exact = true });
    private ILocator QAPhilosophyHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "QA Philosophy", Level = 3 });
    private ILocator PhilosophyText => _page.GetByText("My goal in QA is simple: reduce risk, increase confidence, and keep releases smooth.", new() { Exact = true });

    // Contact Links
    private ILocator GitHubLink => _page.GetByRole(AriaRole.Link, new() { Name = "@cng07" });
    private ILocator LinkedInLink => _page.GetByRole(AriaRole.Link, new() { Name = "@carlosng07" });
    private ILocator EmailLink => _page.GetByRole(AriaRole.Link, new() { Name = "carlosng07@gmail.com" });

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public AboutPage(IPage page)
    {
        _page = page;
    }

    public async Task VerifyAccessibilityElementsAsync()
    {
        var skipToContentHref = await SkipToContentLink.First.GetAttributeAsync("href");
        Assert.That(skipToContentHref, Is.EqualTo("#main-content"));
        await ExpectVisibleAsync(MainContent);
    }

    public async Task VerifyHeaderAsync()
    {
        await ExpectVisibleAsync(AboutMeHeading);
        await ExpectVisibleAsync(IntroductionHeading);
    }

    public async Task VerifyProfileSectionAsync()
    {
        await ExpectVisibleAsync(ExperienceText);
        await ExpectVisibleAsync(MainToolText);
        await ExpectVisibleAsync(ProfileNameHeading);
        await ExpectVisibleAsync(JobTitleText);
        await ExpectVisibleAsync(ProfileImage1);
        await ExpectVisibleAsync(ProfileImage2);
    }

    public async Task VerifyHighlightsAndPhilosophyAsync()
    {
        await ExpectVisibleAsync(FastExecutionText);
        await ExpectVisibleAsync(FastExecutionDetail);
        await ExpectVisibleAsync(MaintainedCodeText);
        await ExpectVisibleAsync(MaintainedCodeDetail);
        await ExpectVisibleAsync(QAPhilosophyHeading);
        await ExpectVisibleAsync(PhilosophyText);
    }

    public async Task VerifyContactLinksAsync()
    {
        await ExpectVisibleAsync(GitHubLink);
        await ExpectVisibleAsync(LinkedInLink);
        await ExpectVisibleAsync(EmailLink);

        var githubHref = await GitHubLink.First.GetAttributeAsync("href");
        Assert.That(githubHref, Is.EqualTo("https://github.com/cng07"));

        var linkedInHref = await LinkedInLink.First.GetAttributeAsync("href");
        Assert.That(linkedInHref, Is.EqualTo("https://www.linkedin.com/in/carlosng07"));

        var emailHref = await EmailLink.First.GetAttributeAsync("href");
        Assert.That(emailHref, Is.EqualTo("mailto:carlosng07@gmail.com"));
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
