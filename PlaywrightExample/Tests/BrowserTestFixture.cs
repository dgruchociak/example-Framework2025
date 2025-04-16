using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core;

namespace Playwright.Tests;

[TestFixture]
public class BrowserTestFixture : IDisposable
{
    protected IBrowser Browser;
    protected IPage Page;
    protected IBrowserContext Context;
    private BrowserFactory _browserFactory;

    [SetUp]
    public async Task SetUp()
    {
        _browserFactory = new BrowserFactory();
        Browser = await _browserFactory.CreateBrowserAsync("chromium", headless: false);
        (Page, Context) = await BrowserFactory.CreatePageAsync(Browser);
        await Page.GotoAsync(Config.CommitQualityUrl);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Browser.CloseAsync();
        // await Context.CloseAsync();
        Dispose();
    }

    public void Dispose()
    {
        _browserFactory?.Dispose();
    }
}