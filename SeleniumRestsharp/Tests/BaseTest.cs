using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumRestsharp.Core;
using SeleniumRestsharp.Pages;

namespace SeleniumRestsharp.Tests;

[TestFixture]
public abstract class BaseTest
{
    private IWebDriver _driver;
    protected Browser Browser;
    private MainPage _mainPage;

    [SetUp]
    public void SetUp()
    {
        _driver = WebDriverFactory.CreateDriver(Config.Browser);
        Browser = new Browser(_driver);
        _mainPage = new MainPage(Browser);

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