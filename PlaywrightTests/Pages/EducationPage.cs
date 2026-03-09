using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class EducationPage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Page Header
    private ILocator EducationHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Education", Level = 1 });
    private ILocator SectionCards => _page.Locator("div.glass");

    // Tertiary Education Section
    private ILocator UniversityHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Asia Pacific College", Level = 2 });
    private ILocator DegreeText => _page.GetByText("BS Electronics Engineering", new() { Exact = true });
    private ILocator GraduationDate => _page.GetByText("June 2013");
    private ILocator UniversityLocation => _page.GetByText("#3 Humabon Place, Magallanes, Makati City, Philippines", new() { Exact = true });
    private ILocator HonorsText => _page.GetByText("Honors & Achievements", new() { Exact = true });
    private ILocator ScholarshipText => _page.GetByText("SCHOLARSHIP");
    private ILocator ScholarshipProvider => _page.GetByText("SM Foundation, Inc.", new() { Exact = true });

    // Leadership Section
    private ILocator LeadershipHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Leadership & Involvement", Level = 2 });
    private ILocator ApeSocietyText => _page.GetByText("APC Society of Electronics Engineering Students", new() { Exact = true });
    private ILocator IecepText => _page.GetByText("Institute of Electronics Engineers of the Philippines (IECEP-Manila Student Chapter)", new() { Exact = true });
    private ILocator ScholarsProgramText => _page.GetByText("APC SM Foundation Inc. Scholars", new() { Exact = true });
    private ILocator MathsSocietyText => _page.GetByText("APC Mathematics Society", new() { Exact = true });

    // Secondary Education Section
    private ILocator SecondaryHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Secondary Education", Level = 2 });
    private ILocator HighSchoolHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Makati Science High School", Level = 2 });
    private ILocator HighSchoolGraduationDate => _page.GetByText("June 2009");
    private ILocator HighSchoolLocation => _page.GetByText("9 Kalayaan Ave, Makati City, Philippines", new() { Exact = true });

    // Publications Section
    private ILocator PublicationsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Publications", Level = 2 });
    private ILocator PaperHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "A Development of a Low-Cost 12-Lead Electrocardiogram Monitoring Device Using Android-Based Smartphone", Level = 3 });
    private ILocator DoiText => _page.GetByText("DOI: 10.1109/GCCE.2018.8574836", new() { Exact = true });
    private ILocator ProceedingsLink => _page.Locator("a[href='https://ieeexplore.ieee.org/xpl/conhome/8555972/proceeding']");
    private ILocator IEEEXploreLink => _page.Locator("a[href='https://ieeexplore.ieee.org/document/8574836']");

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public EducationPage(IPage page)
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
        await ExpectVisibleAsync(EducationHeading);
        Assert.That(await SectionCards.CountAsync(), Is.GreaterThanOrEqualTo(4));
    }

    public async Task VerifyTertiarySectionAsync()
    {
        await ExpectVisibleAsync(UniversityHeading);
        await ExpectVisibleAsync(DegreeText);
        await ExpectVisibleAsync(GraduationDate);
        await ExpectVisibleAsync(UniversityLocation);
        await ExpectVisibleAsync(HonorsText);
        await ExpectVisibleAsync(ScholarshipText);
        await ExpectVisibleAsync(ScholarshipProvider);
    }

    public async Task VerifyLeadershipSectionAsync()
    {
        await ExpectVisibleAsync(LeadershipHeading);
        await ExpectVisibleAsync(ApeSocietyText);
        await ExpectVisibleAsync(IecepText);
        await ExpectVisibleAsync(ScholarsProgramText);
        await ExpectVisibleAsync(MathsSocietyText);
    }

    public async Task VerifySecondarySectionAsync()
    {
        await ExpectVisibleAsync(SecondaryHeading);
        await ExpectVisibleAsync(HighSchoolHeading);
        await ExpectVisibleAsync(HighSchoolGraduationDate);
        await ExpectVisibleAsync(HighSchoolLocation);
    }

    public async Task VerifyPublicationsSectionAsync()
    {
        await ExpectVisibleAsync(PublicationsHeading);
        await ExpectVisibleAsync(PaperHeading);
        await ExpectVisibleAsync(DoiText);
        await ExpectVisibleAsync(ProceedingsLink);
        await ExpectVisibleAsync(IEEEXploreLink);

        var proceedingsHref = await ProceedingsLink.GetAttributeAsync("href");
        Assert.That(proceedingsHref, Is.EqualTo("https://ieeexplore.ieee.org/xpl/conhome/8555972/proceeding"));

        var ieeexploreHref = await IEEEXploreLink.GetAttributeAsync("href");
        Assert.That(ieeexploreHref, Is.EqualTo("https://ieeexplore.ieee.org/document/8574836"));
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
