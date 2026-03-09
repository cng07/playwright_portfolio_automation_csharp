using System.Diagnostics;
using Microsoft.Playwright;

namespace PlaywrightTests.Framework;

public abstract class PortfolioTestBase
{
    protected const string BaseUrl = "https://carlosng07.vercel.app";

    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;
    protected IAPIRequestContext Api = null!;

    [SetUp]
    public async Task SetUpAsync()
    {
        var browsersPath = Path.GetFullPath(Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            "..",
            "..",
            "..",
            "..",
            ".playwright-browsers"));
        Environment.SetEnvironmentVariable("PLAYWRIGHT_BROWSERS_PATH", browsersPath);

        _playwright = await Playwright.CreateAsync();
        var isCi = IsCiEnvironment();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = isCi
        });

        Context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            AcceptDownloads = true
        });

        Page = await Context.NewPageAsync();
        Api = await _playwright.APIRequest.NewContextAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        if (Api is not null)
        {
            await Api.DisposeAsync();
        }

        if (Context is not null)
        {
            await Context.CloseAsync();
        }

        if (_browser is not null)
        {
            await _browser.CloseAsync();
        }

        _playwright?.Dispose();
    }

    protected async Task GoToHomePageAsync()
    {
        await Page.GotoAsync($"{BaseUrl}/");
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await WaitForPathAsync("/");
        await ExpectTitleAsync("Carlos Ng | Portfolio");
        await ExpectVisibleAsync(Page.GetByText("home"));
        await ExpectVisibleAsync(Page.GetByAltText("LinkedIn Logo"));
    }

    protected async Task GoToDirectPathAsync(string path, string title)
    {
        await Page.GotoAsync($"{BaseUrl}{path}");
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await WaitForPathAsync(path);
        await ExpectTitleAsync(title);
    }

    protected async Task WaitForPathAsync(string expectedPath, int timeoutMs = 15000)
    {
        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.ElapsedMilliseconds < timeoutMs)
        {
            if (new Uri(Page.Url).AbsolutePath.Equals(expectedPath, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            await Page.WaitForTimeoutAsync(100);
        }

        Assert.Fail($"Expected path '{expectedPath}', but current URL is '{Page.Url}'.");
    }

    protected async Task ExpectTitleAsync(string expectedTitle)
    {
        await Page.WaitForFunctionAsync("expectedTitle => document.title === expectedTitle", expectedTitle);
        var title = await Page.TitleAsync();
        Assert.That(title, Is.EqualTo(expectedTitle));
    }

    protected static async Task ExpectVisibleAsync(ILocator locator)
    {
        await locator.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
        Assert.That(await locator.First.IsVisibleAsync(), Is.True);
    }

    protected static async Task ExpectAttributeEqualsAsync(ILocator locator, string name, string expectedValue)
    {
        var actual = await locator.First.GetAttributeAsync(name);
        Assert.That(actual, Is.EqualTo(expectedValue));
    }

    protected static async Task ExpectAttributeContainsAsync(ILocator locator, string name, string expectedValuePart)
    {
        var actual = await locator.First.GetAttributeAsync(name);
        Assert.That(actual, Does.Contain(expectedValuePart));
    }

    protected async Task VerifyFooterSectionAsync()
    {
        var privacyPolicyLink = Page.GetByRole(AriaRole.Link, new() { Name = "Privacy Policy" });
        var termsAndConditionsLink = Page.GetByRole(AriaRole.Link, new() { Name = "Terms & Conditions" });

        await ExpectVisibleAsync(privacyPolicyLink);
        await ExpectVisibleAsync(termsAndConditionsLink);
        await ExpectAttributeEqualsAsync(privacyPolicyLink, "href", "/privacy");
        await ExpectAttributeEqualsAsync(termsAndConditionsLink, "href", "/terms");
    }

    protected async Task VerifyInternalPathsApiResponsesAsync(params string[] paths)
    {
        var urls = BuildInternalUrls(paths);
        await VerifyUrlsApiResponsesAsync(urls, timeoutMs: 15000, urlType: "internal URL");
    }

    protected async Task VerifyUrlsApiResponsesAsync(IEnumerable<string> urls, int timeoutMs, string urlType)
    {
        foreach (var url in urls)
        {
            var response = await Api.GetAsync(url, new() { Timeout = timeoutMs });
            Assert.That(response.Status, Is.GreaterThanOrEqualTo(200), $"Expected {urlType} to be reachable: {url}");
            Assert.That(response.Status, Is.LessThan(400), $"Expected {urlType} to be reachable: {url}");
        }
    }

    protected IEnumerable<string> BuildInternalUrls(IEnumerable<string> paths)
    {
        var origin = new Uri(Page.Url).GetLeftPart(UriPartial.Authority);
        return paths.Select(path => $"{origin}{(path.StartsWith("/") ? path : $"/{path}")}");
    }

    protected static string GetHeader(IAPIResponse response, string headerName)
    {
        var header = response.Headers.FirstOrDefault(h => h.Key.Equals(headerName, StringComparison.OrdinalIgnoreCase));
        return header.Value ?? string.Empty;
    }

    private static bool IsCiEnvironment()
    {
        var ci = Environment.GetEnvironmentVariable("CI");
        if (string.IsNullOrWhiteSpace(ci))
        {
            return false;
        }

        return ci.Equals("true", StringComparison.OrdinalIgnoreCase) || ci == "1";
    }
}
