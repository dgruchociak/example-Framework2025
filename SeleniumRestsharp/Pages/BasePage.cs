using OpenQA.Selenium;
using SeleniumRestsharp.Core;

namespace SeleniumRestsharp.Pages;

public abstract class BasePage(Browser browser)
{
    protected readonly Browser Browser = browser;

    protected virtual IWebElement GetRandomDisplayedWebElement(By locator)
    {
        Browser.WaitForPageLoad();
        var list = Browser.FindElements(locator);
        var displayedList = list.Where(item => item.Displayed).ToList();
        var rnd = new Random();
        var ele = rnd.Next(displayedList.Count);
        
        return displayedList[ele];
    }

    public abstract bool IsDisplayed();

    public abstract T? AcceptCookies<T>() where T : class;
}