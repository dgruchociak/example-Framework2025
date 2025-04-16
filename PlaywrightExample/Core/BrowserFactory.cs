using Microsoft.Playwright;

namespace Playwright.Core;

public class BrowserFactory
{
    private readonly IPlaywright _playwright = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();

    public async Task<IBrowser> CreateBrowserAsync(string browserType = "chromium", bool headless = true)
    {
        IBrowser browser;
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Channel = "chrome",
            Headless = headless
        };

        switch (browserType.ToLower())
        {
            case "chromium":
                browser = await _playwright.Chromium.LaunchAsync(launchOptions);
                break;
            case "firefox":
                browser = await _playwright.Firefox.LaunchAsync(launchOptions);
                break;
            case "webkit":
                browser = await _playwright.Webkit.LaunchAsync(launchOptions);
                break;
            default:
                throw new ArgumentException("Invalid browser type specified", nameof(browserType));
        }

        return browser;
    }

    public static async Task<(IPage, IBrowserContext)> CreatePageAsync(IBrowser browser)
    {
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();
        return (page, context);
    }

    public void Dispose()
    {
        _playwright?.Dispose();
    }
}