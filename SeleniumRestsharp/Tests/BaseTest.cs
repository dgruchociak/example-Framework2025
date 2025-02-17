using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAndRestSharp.Core;
using SeleniumAndRestSharp.Pages;

namespace SeleniumAndRestSharp.Tests;

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