using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ResumePage
{
    private readonly IPage _page;
    private readonly string _baseUrl;

    // Accessibility Elements
    private ILocator SkipToContentLink => _page.GetByRole(AriaRole.Link, new() { Name = "Skip to content" });
    private ILocator MainContent => _page.Locator("#main-content");

    // Page Header
    private ILocator ResumeHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Resume", Level = 1 });

    // Download Button
    private ILocator DownloadButton => _page.GetByRole(AriaRole.Button, new() { Name = "Download PDF" });

    // Resume Viewer
    private ILocator ResumePdfObject => _page.Locator("object[type='application/pdf']");
    private ILocator ResumePdfIframe => _page.Locator("object iframe[title='Carlos Ng Resume']");

    // Footer Elements
    private ILocator PrivacyPolicyLink => _page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
    private ILocator TermsAndConditionsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

    public ResumePage(IPage page, string baseUrl)
    {
        _page = page;
        _baseUrl = baseUrl;
    }

    public async Task VerifyAccessibilityElementsAsync()
    {
        var skipToContentHref = await SkipToContentLink.First.GetAttributeAsync("href");
        Assert.That(skipToContentHref, Is.EqualTo("#main-content"));
        await ExpectVisibleAsync(MainContent);
    }

    public async Task VerifyResumeHeaderAsync()
    {
        await ExpectVisibleAsync(ResumeHeading);
    }

    public async Task VerifyDownloadPdfButtonAsync()
    {
        await ExpectVisibleAsync(DownloadButton);
        Assert.That(await DownloadButton.IsEnabledAsync(), Is.True);
    }

    public async Task VerifyResumeViewerSectionAsync()
    {
        await ExpectVisibleAsync(ResumePdfObject);

        var dataAttribute = await ResumePdfObject.First.GetAttributeAsync("data");
        Assert.That(dataAttribute, Is.EqualTo("/Carlos_Ng_Resume.pdf"));

        var iframeSrc = await ResumePdfIframe.First.GetAttributeAsync("src");
        Assert.That(iframeSrc, Does.Contain("docs.google.com/viewer"));
        Assert.That(iframeSrc, Does.Contain("Carlos_Ng_Resume.pdf"));
    }

    public async Task DownloadPdfAndVerifyAsync(IAPIRequestContext api)
    {
        var downloadTask = _page.WaitForDownloadAsync();
        await DownloadButton.ClickAsync();

        var download = await downloadTask;
        Assert.That(download.SuggestedFilename, Is.EqualTo("Carlos_Ng_Resume.pdf"));
        Assert.That(download.Url, Does.Contain("/Carlos_Ng_Resume.pdf"));

        var apiResponse = await api.GetAsync(download.Url, new() { Timeout = 30000 });
        Assert.That(apiResponse.Status, Is.EqualTo(200));
        Assert.That(GetHeader(apiResponse, "content-type"), Does.Contain("application/pdf"));

        var filePath = await download.PathAsync();
        Assert.That(filePath, Is.Not.Null.And.Not.Empty);

        var fileInfo = new FileInfo(filePath!);
        var fileSizeKb = fileInfo.Length / 1024d;
        Assert.That(fileSizeKb, Is.GreaterThan(500));
        Assert.That(fileSizeKb, Is.LessThan(1000));

        File.Delete(filePath!);
    }

    public async Task VerifyResumePdfApiResponseAsync(IAPIRequestContext api)
    {
        var response = await api.GetAsync($"{_baseUrl}/Carlos_Ng_Resume.pdf", new() { Timeout = 30000 });
        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(GetHeader(response, "content-type"), Does.Contain("application/pdf"));

        var contentLength = long.TryParse(GetHeader(response, "content-length"), out var value) ? value : 0;
        Assert.That(contentLength, Is.GreaterThan(500000));
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

    private static string? GetHeader(IAPIResponse response, string headerName)
    {
        var headers = response.Headers;
        return headers.TryGetValue(headerName, out var value) ? value : null;
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
