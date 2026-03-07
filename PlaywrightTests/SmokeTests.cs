using Microsoft.Playwright;

namespace PlaywrightTests;

public class SmokeTests
{
    [Test]
    public async Task HomePageLoadsAndHasPlaywrightTitle()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://playwright.dev/");

        var title = await page.TitleAsync();
        Assert.That(title, Does.Contain("Playwright"));
    }
}
