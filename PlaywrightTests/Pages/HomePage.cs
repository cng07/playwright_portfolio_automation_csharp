using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class HomePage
{
    private readonly IPage _page;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Navigation Bar Elements
    private ILocator NavLogo => _page.Locator("a.nav-logo");
    private ILocator NavHomeLink => _page.GetByRole(AriaRole.Link, new() { Name = "Home" });
    private ILocator NavProjectsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Projects", Exact = true });
    private ILocator NavResumeLink => _page.GetByRole(AriaRole.Link, new() { Name = "Resume" });
    private ILocator NavAboutLink => _page.GetByRole(AriaRole.Link, new() { Name = "About" });
    private ILocator NavContactLink => _page.GetByRole(AriaRole.Link, new() { Name = "Contact" });
    private ILocator NavMore => _page.GetByRole(AriaRole.Button, new() { Name = "More" });
    private ILocator NavCertificationsLink => _page.GetByRole(AriaRole.Menuitem, new() { Name = "Certifications" });
    private ILocator NavEducationLink => _page.GetByRole(AriaRole.Menuitem, new() { Name = "Education" });

    // Hero Section Elements
    private ILocator HeroImage => _page.GetByAltText("Carlos Angelo E. Ng");
    private ILocator HeroSubHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Carlos Angelo E. Ng", Level = 2 });
    private ILocator HeroMainHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Automating Quality Delivering Excellence", Level = 1 });
    private ILocator HeroJobTitle => _page.GetByText("Senior Quality Assurance Automation Engineer at Datacom");
    private ILocator HeroIntro => _page.GetByText("Hi, I'm Carlos Ng.");

    // Social Media Elements
    private ILocator LinkedInLogo => _page.GetByAltText("LinkedIn Logo");
    private ILocator GitHubLogo => _page.GetByAltText("GitHub Logo");
    private ILocator IEEELogo => _page.GetByAltText("IEEE Logo");
    private ILocator ASTQBLogo => _page.GetByAltText("ASTQB Logo");

    // Featured Projects Section
    private ILocator RepositoryLinks => _page.GetByRole(AriaRole.Link, new() { Name = "Repository" });
    private ILocator ViewAllProjectsLink => _page.GetByRole(AriaRole.Link, new() { Name = "View All Projects" });
    private ILocator FeaturedProjectsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Featured Projects", Level = 2 });
    private ILocator TypeScriptProjectHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Portfolio Website Automation (TypeScript)", Level = 3 });
    private ILocator PythonProjectHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Portfolio Website Automation (Python)", Level = 3 });

    // Skills Section
    private ILocator SkillsSectionTitle => _page.Locator("h2:has-text(\"Technical Skills\")");
    private ILocator TestAutomationSkill => _page.Locator("h3:has-text(\"Test Automation\")");
    private ILocator ProgrammingLanguagesSkill => _page.Locator("h3:has-text(\"Programming Languages\")");
    private ILocator CICDSkill => _page.Locator("h3:has-text(\"CI/CD\")");
    private ILocator ManualTestingSkill => _page.Locator("h3:has-text(\"Manual Testing\")");
    private ILocator OtherToolsSkill => _page.Locator("h3:has-text(\"Other Tools\")");
    private ILocator AIToolsSkill => _page.Locator("h3:has-text(\"AI Tools\")");

    // Certifications Section
    private ILocator CertificationsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Certifications", Level = 2 });
    private ILocator CtflPreview => _page.GetByText("ISTQB Certified Tester Foundation Level (CTFL)", new() { Exact = true });
    private ILocator ViewCertificateLink => _page.GetByRole(AriaRole.Link, new() { Name = "View Certificate" }).First;
    private ILocator ViewAllCertificationsLink => _page.GetByRole(AriaRole.Link, new() { Name = "View All 4 Certifications" });

    // Publication Section
    private ILocator PublicationHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Publication", Level = 2 });
    private ILocator PublicationTitle => _page.GetByText("A Development of a Low-Cost 12-Lead Electrocardiogram Monitoring Device Using Android-based Smartphone", new() { Exact = true });
    private ILocator PublicationDate => _page.GetByText("Published in IEEE, 2018", new() { Exact = true });
    private ILocator ViewPaperLink => _page.GetByRole(AriaRole.Link, new() { Name = "View Paper" });

    // Experience Section
    private ILocator ExperienceHeading => _page.Locator("h2:has-text(\"Experience\")");
    private ILocator DatacomLink => _page.GetByRole(AriaRole.Link, new() { Name = "Datacom", Exact = true });
    private ILocator PlanitLink => _page.GetByRole(AriaRole.Link, new() { Name = "Planit", Exact = true });
    private ILocator DXCTechnologyLink => _page.GetByRole(AriaRole.Link, new() { Name = "DXC Technology", Exact = true });
    private ILocator DataAnalyticsVenturesLink => _page.GetByRole(AriaRole.Link, new() { Name = "Data Analytics Ventures, Inc.", Exact = true });
    private ILocator AccentureLinks => _page.GetByRole(AriaRole.Link, new() { Name = "Accenture", Exact = true });

    // Footer Section
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public HomePage(IPage page)
    {
        _page = page;
    }

    // Verification Methods
    public async Task VerifyAccessibilityElementsAsync()
    {
        var skipToContentHref = await SkipToContentLink.First.GetAttributeAsync("href");
        Assert.That(skipToContentHref, Is.EqualTo("#main-content"));
        await ExpectVisibleAsync(MainContent);
    }

    public async Task VerifyNavigationBarSectionAsync()
    {
        await ExpectVisibleAsync(NavLogo);
        await ExpectVisibleAsync(NavHomeLink);
        await ExpectVisibleAsync(NavProjectsLink);
        await ExpectVisibleAsync(NavResumeLink);
        await ExpectVisibleAsync(NavAboutLink);
        await ExpectVisibleAsync(NavContactLink);

        var projectsHref = await NavProjectsLink.First.GetAttributeAsync("href");
        Assert.That(projectsHref, Is.EqualTo("/projects"));

        var resumeHref = await NavResumeLink.First.GetAttributeAsync("href");
        Assert.That(resumeHref, Is.EqualTo("/resume"));

        var aboutHref = await NavAboutLink.First.GetAttributeAsync("href");
        Assert.That(aboutHref, Is.EqualTo("/about"));

        var contactHref = await NavContactLink.First.GetAttributeAsync("href");
        Assert.That(contactHref, Is.EqualTo("/contact"));

        await ExpectVisibleAsync(NavMore);
        await NavMore.ClickAsync();
        await ExpectVisibleAsync(NavCertificationsLink);
        await ExpectVisibleAsync(NavEducationLink);

        var certificationsHref = await NavCertificationsLink.First.GetAttributeAsync("href");
        Assert.That(certificationsHref, Is.EqualTo("/certifications"));

        var educationHref = await NavEducationLink.First.GetAttributeAsync("href");
        Assert.That(educationHref, Is.EqualTo("/education"));
    }

    public async Task VerifyHeroSectionAsync()
    {
        await ExpectVisibleAsync(HeroImage);
        await ExpectVisibleAsync(HeroSubHeading);
        await ExpectVisibleAsync(HeroMainHeading);
        await ExpectVisibleAsync(HeroJobTitle);
        await ExpectVisibleAsync(HeroIntro);
    }

    public async Task VerifySocialMediaSectionAsync()
    {
        await ExpectVisibleAsync(LinkedInLogo);
        await ExpectVisibleAsync(GitHubLogo);
        await ExpectVisibleAsync(IEEELogo);
        await ExpectVisibleAsync(ASTQBLogo);
    }

    public async Task VerifyFeaturedProjectsSectionAsync()
    {
        await ExpectVisibleAsync(FeaturedProjectsHeading);
        await ExpectVisibleAsync(TypeScriptProjectHeading);
        await ExpectVisibleAsync(PythonProjectHeading);
        await ExpectVisibleAsync(ViewAllProjectsLink);

        var projectsHref = await ViewAllProjectsLink.First.GetAttributeAsync("href");
        Assert.That(projectsHref, Is.EqualTo("/projects"));

        var repositoryLinkCount = await RepositoryLinks.CountAsync();
        Assert.That(repositoryLinkCount, Is.GreaterThanOrEqualTo(2));
        await ExpectVisibleAsync(RepositoryLinks.Nth(0));
        await ExpectVisibleAsync(RepositoryLinks.Nth(1));
    }

    public async Task VerifySkillsSectionAsync()
    {
        await ExpectVisibleAsync(SkillsSectionTitle);
        var skillsText = await SkillsSectionTitle.InnerTextAsync();
        Assert.That(skillsText, Does.Contain("Technical Skills"));

        await ExpectVisibleAsync(TestAutomationSkill);
        await ExpectVisibleAsync(ProgrammingLanguagesSkill);
        await ExpectVisibleAsync(CICDSkill);
        await ExpectVisibleAsync(ManualTestingSkill);
        await ExpectVisibleAsync(OtherToolsSkill);
        await ExpectVisibleAsync(AIToolsSkill);
    }

    public async Task VerifyCertificationsSectionAsync()
    {
        await ExpectVisibleAsync(CertificationsHeading);
        await ExpectVisibleAsync(CtflPreview);
        await ExpectVisibleAsync(ViewCertificateLink);
        await ExpectVisibleAsync(ViewAllCertificationsLink);

        var certificationsHref = await ViewAllCertificationsLink.First.GetAttributeAsync("href");
        Assert.That(certificationsHref, Is.EqualTo("/certifications"));
    }

    public async Task VerifyPublicationSectionAsync()
    {
        await ExpectVisibleAsync(PublicationHeading);
        await ExpectVisibleAsync(PublicationTitle);
        await ExpectVisibleAsync(PublicationDate);
        await ExpectVisibleAsync(ViewPaperLink);

        var paperHref = await ViewPaperLink.First.GetAttributeAsync("href");
        Assert.That(paperHref, Does.Contain("ieeexplore.ieee.org"));
    }

    public async Task VerifyExperienceSectionAsync()
    {
        await ExpectVisibleAsync(ExperienceHeading);
        var experienceText = await ExperienceHeading.InnerTextAsync();
        Assert.That(experienceText, Does.Contain("Experience"));

        await ExpectVisibleAsync(DatacomLink);
        await ExpectVisibleAsync(PlanitLink);
        await ExpectVisibleAsync(DXCTechnologyLink);
        await ExpectVisibleAsync(DataAnalyticsVenturesLink);
        await ExpectVisibleAsync(AccentureLinks.Nth(0));
        await ExpectVisibleAsync(AccentureLinks.Nth(1));
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

    // Helper method for visibility checks
    private static async Task ExpectVisibleAsync(ILocator locator)
    {
        await locator.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
        Assert.That(await locator.First.IsVisibleAsync(), Is.True);
    }
}
