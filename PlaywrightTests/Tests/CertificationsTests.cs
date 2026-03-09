using PlaywrightTests.Framework;
using PlaywrightTests.Pages;
using Microsoft.Playwright;

namespace PlaywrightTests.Tests;

[TestFixture]
public class CertificationsTests : PortfolioTestBase
{
    private CertificationsPage _certificationsPage = null!;

    [SetUp]
    public new async Task SetUpAsync()
    {
        await base.SetUpAsync();
        _certificationsPage = new CertificationsPage(Page);
    }

    [Test]
    public async Task VerifyCertificationsPageViaNavigationMenu()
    {
        await GoToHomePageAsync();
        await GoToCertificationsPageFromHomeNavigationAsync();
        await VerifyAllCertificationsElementsAsync();
    }

    [Test]
    public async Task VerifyCertificationsPageViaDirectUrl()
    {
        await GoToDirectPathAsync("/certifications", "Carlos Ng | Certifications");
        await VerifyAllCertificationsElementsAsync();
    }

    private async Task GoToCertificationsPageFromHomeNavigationAsync()
    {
        var moreButton = Page.GetByRole(AriaRole.Button, new() { Name = "More" });
        await ExpectVisibleAsync(moreButton);
        await moreButton.ClickAsync();

        var certificationsMenuItem = Page.GetByRole(AriaRole.Menuitem, new() { Name = "Certifications" });
        await ExpectVisibleAsync(certificationsMenuItem);
        await certificationsMenuItem.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await WaitForPathAsync("/certifications");
        await ExpectTitleAsync("Carlos Ng | Certifications");
    }

    private async Task VerifyAllCertificationsElementsAsync()
    {
        await _certificationsPage.VerifyPageHeaderAsync();
        await _certificationsPage.VerifyCTFLCertificationAsync();
        await _certificationsPage.VerifyDevOpsCertificationAsync();
        await _certificationsPage.VerifyAccentureAgileCertificationAsync();
        await _certificationsPage.VerifyAutomationAnywhereCertificationAsync();
        await _certificationsPage.VerifyCertificationLinksAsync();
    }
}
