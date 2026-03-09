using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ExperiencePage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Page Header
    private ILocator ExperienceHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Work Experience", Level = 1 });
    private ILocator ExperienceCards => _page.Locator("div.glass");

    // Experience Entries
    private ILocator DatacomHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Datacom", Level = 3 });
    private ILocator PlanitHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Planit", Level = 3 });
    private ILocator DXCHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "DXC Technology", Level = 3 });
    private ILocator DAVIHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Data Analytics Ventures, Inc.", Level = 3 });
    private ILocator AccentureHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Accenture", Level = 3 });

    // Job Title and Dates
    private ILocator JobTitleText => _page.GetByText("Senior Quality Assurance Automation Engineer");
    private ILocator JobDateText => _page.GetByText("April 2025");
    private ILocator TaguigLocationText => _page.GetByText("Taguig City, Philippines", new() { Exact = true }).Nth(0);
    private ILocator PasigLocationText => _page.GetByText("Pasig City, Philippines", new() { Exact = true });
    private ILocator MandaluyongLocationText => _page.GetByText("Mandaluyong City, Philippines", new() { Exact = true }).Nth(0);

    // Company Links
    private ILocator DatacomLink => _page.GetByRole(AriaRole.Link, new() { Name = "Datacom", Exact = true });
    private ILocator PlanitLink => _page.GetByRole(AriaRole.Link, new() { Name = "Planit", Exact = true });
    private ILocator DXCLink => _page.GetByRole(AriaRole.Link, new() { Name = "DXC Technology", Exact = true });
    private ILocator DAVILink => _page.GetByRole(AriaRole.Link, new() { Name = "Data Analytics Ventures, Inc.", Exact = true });
    private ILocator AccentureLink => _page.GetByRole(AriaRole.Link, new() { Name = "Accenture", Exact = true });

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public ExperiencePage(IPage page)
    {
        _page = page;
    }

    public async Task VerifyAccessibilityElementsAsync()
    {
        var skipToContentHref = await SkipToContentLink.First.GetAttributeAsync("href");
        Assert.That(skipToContentHref, Is.EqualTo("#main-content"));
        await ExpectVisibleAsync(MainContent);
    }

    public async Task VerifyPageHeaderAsync()
    {
        await ExpectVisibleAsync(ExperienceHeading);
        Assert.That(await ExperienceCards.CountAsync(), Is.GreaterThanOrEqualTo(6));
    }

    public async Task VerifyExperienceEntriesAsync()
    {
        await ExpectVisibleAsync(DatacomHeading);
        await ExpectVisibleAsync(PlanitHeading);
        await ExpectVisibleAsync(DXCHeading);
        await ExpectVisibleAsync(DAVIHeading);
        await ExpectVisibleAsync(AccentureHeading.Nth(0));
        await ExpectVisibleAsync(AccentureHeading.Nth(1));

        await ExpectVisibleAsync(JobTitleText);
        await ExpectVisibleAsync(JobDateText);
        await ExpectVisibleAsync(TaguigLocationText);
        await ExpectVisibleAsync(PasigLocationText);
        await ExpectVisibleAsync(MandaluyongLocationText);
    }

    public async Task VerifyCompanyLinksAsync()
    {
        await ExpectVisibleAsync(DatacomLink);
        await ExpectVisibleAsync(PlanitLink);
        await ExpectVisibleAsync(DXCLink);
        await ExpectVisibleAsync(DAVILink);
        await ExpectVisibleAsync(AccentureLink.Nth(0));
        await ExpectVisibleAsync(AccentureLink.Nth(1));

        var datacomHref = await DatacomLink.First.GetAttributeAsync("href");
        Assert.That(datacomHref, Is.EqualTo("https://datacom.com/nz/en"));

        var planitHref = await PlanitLink.First.GetAttributeAsync("href");
        Assert.That(planitHref, Is.EqualTo("https://www.planit.com/"));

        var dxcHref = await DXCLink.First.GetAttributeAsync("href");
        Assert.That(dxcHref, Is.EqualTo("https://dxc.com/"));

        var daviHref = await DAVILink.First.GetAttributeAsync("href");
        Assert.That(daviHref, Is.EqualTo("https://www.davi.com.ph/"));

        var accentureHref1 = await AccentureLink.Nth(0).GetAttributeAsync("href");
        Assert.That(accentureHref1, Is.EqualTo("https://www.accenture.com/ph-en"));

        var accentureHref2 = await AccentureLink.Nth(1).GetAttributeAsync("href");
        Assert.That(accentureHref2, Is.EqualTo("https://www.accenture.com/ph-en"));
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
