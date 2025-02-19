using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumRestsharp.Core;
using SeleniumRestsharp.Pages;

namespace SeleniumRestsharp.Tests;

[TestFixture]
//TODO Add parallelization
// [Parallelizable(ParallelScope.All)]
public abstract class BaseTest
{
    private IWebDriver _driver;
    protected Browser Browser;
    private MainPage _mainPage;
    private WebDriverFactory _webDriverFactory;

    [SetUp]
    public void SetUp()
    {
        _webDriverFactory = new WebDriverFactory();
        Browser = new Browser(_driver);
        _mainPage = new MainPage(Browser);
        _driver = _webDriverFactory.CreateDriver(Config.Browser);
        
        try
        {
            _driver.Manage().Window.Maximize();
            Browser.GoTo(Config.BaseUrl);
            Browser.WaitForPageLoad(_driver);
            _mainPage.AcceptCookies<MainPage>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}