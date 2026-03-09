using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class CertificationsPage
{
    private readonly IPage _page;

    // Page Header
    private ILocator CertificationsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Certifications", Level = 1 });

    // CTFL Certification
    private ILocator CtflHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "ISTQB Certified Tester Foundation Level (CTFL)", Level = 2 });
    private ILocator CtflDate => _page.GetByText("April 2024", new() { Exact = true });
    private ILocator CtflExpiry => _page.GetByText("No Expiry", new() { Exact = true }).Nth(0);
    private ILocator CtflIssuer => _page.GetByText("ASTQB - ISTQB in the U.S.", new() { Exact = true }).Nth(0);
    private ILocator CtflCredentialLabel => _page.GetByText("Credential ID:", new() { Exact = true }).Nth(0);
    private ILocator CtflCredentialId => _page.GetByText("24-CTFL-01347-USA", new() { Exact = true });

    // DevOps Certification
    private ILocator DevOpsHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Certified Tester, AT*SQA DevOps Testing", Level = 2 });
    private ILocator DevOpsDate => _page.GetByText("January 2023").First;
    private ILocator DevOpsExpiry => _page.GetByText("Expired", new() { Exact = true }).Nth(0);
    private ILocator DevOpsIssuer => _page.GetByText("ASTQB - ISTQB in the U.S.", new() { Exact = true }).Nth(1);
    private ILocator DevOpsCredentialLabel => _page.GetByText("Credential ID:", new() { Exact = true }).Nth(1);
    private ILocator DevOpsCredentialId => _page.GetByText("23-AT*DevOps-00002-USA", new() { Exact = true });

    // Accenture Agile Certification
    private ILocator AccentureAgileHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Accenture Agile Certification Program", Level = 2 });
    private ILocator AccentureAgileTitle => _page.GetByText("Agile Professional Certified", new() { Exact = true });
    private ILocator AccentureAgileDate => _page.GetByText("June 2020", new() { Exact = true });
    private ILocator AccentureAgileExpiry => _page.GetByText("No Expiry", new() { Exact = true }).Nth(1);
    private ILocator AccentureAgileIssuer => _page.GetByText("Accenture", new() { Exact = true });
    private ILocator AccentureAgileCertificateLabel => _page.GetByText("Certificate Number:", new() { Exact = true }).Nth(0);
    private ILocator AccentureAgileCertificateId => _page.GetByText("CNAG0000009961", new() { Exact = true });

    // Automation Anywhere Certification
    private ILocator AutomationAnywhereHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Automation Anywhere Certified Advanced RPA Professional", Level = 2 });
    private ILocator AutomationAnywhereTitle => _page.GetByText("Robotic Process Automation Professional (V11.0)", new() { Exact = true });
    private ILocator AutomationAnywhereDate => _page.GetByText("July 2020").First;
    private ILocator AutomationAnywhereExpiry => _page.GetByText("Expired", new() { Exact = true }).Nth(1);
    private ILocator AutomationAnywhereIssuer => _page.GetByText("Automation Anywhere", new() { Exact = true });
    private ILocator AutomationAnywhereCertificateLabel => _page.GetByText("Certificate Number:", new() { Exact = true }).Nth(1);
    private ILocator AutomationAnywhereCertificateId => _page.GetByText("AAADVC-21147163", new() { Exact = true });

    // Certification Links
    private ILocator CertifiedTestersList => _page.GetByRole(AriaRole.Link, new() { Name = "Official U.S. List of Certified & Credentialed Software Testers" });
    private ILocator ViewCertificateLinks => _page.Locator("a:has-text('View Certificate')");

    public CertificationsPage(IPage page)
    {
        _page = page;
    }

    public async Task VerifyPageHeaderAsync()
    {
        await ExpectVisibleAsync(CertificationsHeading);
    }

    public async Task VerifyCTFLCertificationAsync()
    {
        await ExpectVisibleAsync(CtflHeading);
        await ExpectVisibleAsync(CtflDate);
        await ExpectVisibleAsync(CtflExpiry);
        await ExpectVisibleAsync(CtflIssuer);
        await ExpectVisibleAsync(CtflCredentialLabel);
        await ExpectVisibleAsync(CtflCredentialId);
    }

    public async Task VerifyDevOpsCertificationAsync()
    {
        await ExpectVisibleAsync(DevOpsHeading);
        await ExpectVisibleAsync(DevOpsDate);
        await ExpectVisibleAsync(DevOpsExpiry);
        await ExpectVisibleAsync(DevOpsIssuer);
        await ExpectVisibleAsync(DevOpsCredentialLabel);
        await ExpectVisibleAsync(DevOpsCredentialId);
    }

    public async Task VerifyAccentureAgileCertificationAsync()
    {
        await ExpectVisibleAsync(AccentureAgileHeading);
        await ExpectVisibleAsync(AccentureAgileTitle);
        await ExpectVisibleAsync(AccentureAgileDate);
        await ExpectVisibleAsync(AccentureAgileExpiry);
        await ExpectVisibleAsync(AccentureAgileIssuer);
        await ExpectVisibleAsync(AccentureAgileCertificateLabel);
        await ExpectVisibleAsync(AccentureAgileCertificateId);
    }

    public async Task VerifyAutomationAnywhereCertificationAsync()
    {
        await ExpectVisibleAsync(AutomationAnywhereHeading);
        await ExpectVisibleAsync(AutomationAnywhereTitle);
        await ExpectVisibleAsync(AutomationAnywhereDate);
        await ExpectVisibleAsync(AutomationAnywhereExpiry);
        await ExpectVisibleAsync(AutomationAnywhereIssuer);
        await ExpectVisibleAsync(AutomationAnywhereCertificateLabel);
        await ExpectVisibleAsync(AutomationAnywhereCertificateId);
    }

    public async Task VerifyCertificationLinksAsync()
    {
        await ExpectVisibleAsync(CertifiedTestersList);
        Assert.That(await ViewCertificateLinks.CountAsync(), Is.EqualTo(4));
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
