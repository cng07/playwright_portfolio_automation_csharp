using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ContactPage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Page Header
    private ILocator GetInTouchHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Get in Touch", Level = 1 });
    private ILocator ContactDescription => _page.GetByText("Let's connect to exchange ideas and discuss topics related to software engineering and innovation.", new() { Exact = true });

    // Contact Methods
    private ILocator EmailHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Email", Level = 3 });
    private ILocator LinkedInHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "LinkedIn", Level = 3 });
    private ILocator GitHubHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "GitHub", Level = 3 });
    private ILocator IEEEHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "IEEE Xplore", Level = 3 });
    private ILocator AtsqaHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "AT*SQA Profile", Level = 3 });

    // Contact Links
    private ILocator EmailLink => _page.Locator("#main-content a[href='mailto:carlosng07@gmail.com']").First;
    private ILocator LinkedInLink => _page.Locator("#main-content a[href='https://www.linkedin.com/in/carlosng07']").First;
    private ILocator GitHubLink => _page.Locator("#main-content a[href='https://github.com/cng07']").First;
    private ILocator IEEELink => _page.Locator("#main-content a[href='https://ieeexplore.ieee.org/author/37086553247']").First;
    private ILocator AtsqaLink => _page.Locator("#main-content a[href='https://atsqa.org/certified-testers/profile/6676da6cab1b424aa4070395ff71f490']").First;

    // Contact CTA Text
    private ILocator LinkedInCTA => _page.GetByText("Connect on LinkedIn", new() { Exact = true });
    private ILocator GitHubCTA => _page.GetByText("Follow on GitHub", new() { Exact = true });
    private ILocator IEEEECta => _page.GetByText("View Publications", new() { Exact = true });
    private ILocator AtsqaCTA => _page.GetByText("View Certified Tester Profile", new() { Exact = true });

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public ContactPage(IPage page)
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
        await ExpectVisibleAsync(GetInTouchHeading);
        await ExpectVisibleAsync(ContactDescription);
    }

    public async Task VerifyContactMethodsSectionAsync()
    {
        await ExpectVisibleAsync(EmailHeading);
        await ExpectVisibleAsync(LinkedInHeading);
        await ExpectVisibleAsync(GitHubHeading);
        await ExpectVisibleAsync(IEEEHeading);
        await ExpectVisibleAsync(AtsqaHeading);

        await ExpectVisibleAsync(EmailLink);
        await ExpectVisibleAsync(LinkedInLink);
        await ExpectVisibleAsync(GitHubLink);
        await ExpectVisibleAsync(IEEELink);
        await ExpectVisibleAsync(AtsqaLink);

        var emailHref = await EmailLink.GetAttributeAsync("href");
        Assert.That(emailHref, Is.EqualTo("mailto:carlosng07@gmail.com"));

        var linkedInHref = await LinkedInLink.GetAttributeAsync("href");
        Assert.That(linkedInHref, Is.EqualTo("https://www.linkedin.com/in/carlosng07"));

        var gitHubHref = await GitHubLink.GetAttributeAsync("href");
        Assert.That(gitHubHref, Is.EqualTo("https://github.com/cng07"));

        var ieeeHref = await IEEELink.GetAttributeAsync("href");
        Assert.That(ieeeHref, Is.EqualTo("https://ieeexplore.ieee.org/author/37086553247"));

        var atsqaHref = await AtsqaLink.GetAttributeAsync("href");
        Assert.That(atsqaHref, Is.EqualTo("https://atsqa.org/certified-testers/profile/6676da6cab1b424aa4070395ff71f490"));

        await ExpectVisibleAsync(LinkedInCTA);
        await ExpectVisibleAsync(GitHubCTA);
        await ExpectVisibleAsync(IEEEECta);
        await ExpectVisibleAsync(AtsqaCTA);
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
