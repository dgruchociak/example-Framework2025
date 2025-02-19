using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core;

namespace Playwright.Tests;

[TestFixture]
public class BrowserTestFixture : IDisposable
{
    protected IBrowser Browser;
    protected IPage Page;
    private BrowserFactory _browserFactory;

    [SetUp]
    public async Task SetUp()
    {
        _browserFactory = new BrowserFactory();
        Browser = await _browserFactory.CreateBrowserAsync("chromium", headless: false);
        Page = await _browserFactory.CreatePageAsync(Browser);
        await Page.GotoAsync(Config.BaseUrl);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Browser.CloseAsync();
        Dispose();
    }

    public void Dispose()
    {
        _browserFactory?.Dispose();
    }
}