using PlaywrightTests.Framework;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests;

[TestFixture]
public class HomeTests : PortfolioTestBase
{
    private HomePage _homePage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _homePage = new HomePage(Page);
    }

    [Test]
    public async Task VerifyHomePage()
    {
        await GoToHomePageAsync();
        await _homePage.VerifyAccessibilityElementsAsync();
        await _homePage.VerifyNavigationBarSectionAsync();
        await _homePage.VerifyHeroSectionAsync();
        await _homePage.VerifySocialMediaSectionAsync();
        await _homePage.VerifyFeaturedProjectsSectionAsync();
        await _homePage.VerifySkillsSectionAsync();
        await _homePage.VerifyCertificationsSectionAsync();
        await _homePage.VerifyPublicationSectionAsync();
        await _homePage.VerifyExperienceSectionAsync();
        await _homePage.VerifyFooterSectionAsync();
    }
}
