using OpenQA.Selenium;
using SeleniumAndRestSharp.Core;

namespace SeleniumAndRestSharp.Pages;

public abstract class BasePage(Browser browser)
{
    protected readonly Browser Browser = browser;

    public abstract bool IsDisplayed();

    public abstract T? AcceptCookies<T>() where T : class;
}