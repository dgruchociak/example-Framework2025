using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumRestsharp.Core;

public class Browser
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public Browser(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Config.DefaultTimeout));
    }
    
    public string GetText(By locator) => FindElement(locator).Text;
    
    public string GetTitle() => _driver.Title;

    public void GoTo(string url) => _driver.Navigate().GoToUrl(url);

    public IWebElement FindElement(By locator) => _wait.Until(d => d.FindElement(locator));
    
    public ReadOnlyCollection<IWebElement> FindElements(By locator) => _wait.Until(d => d.FindElements(locator));

    public void Click(By locator) => WaitForElement(locator).Click();
    public void Click(IWebElement ele) => WaitForElement(ele).Click();
    
    public void SendKeys(By locator, string text) => WaitForElement(locator).SendKeys(text);
    
    public IWebElement WaitForElement(By locator) => _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    
    public IWebElement WaitForElement(IWebElement ele) => _wait.Until(ExpectedConditions.ElementToBeClickable(ele));

    public void ClickIfDisplayed(By locator)
    {
        try
        {
            var element = _driver.FindElement(locator);
            if (element.Displayed)
            {
                element.Click();
            }
        }
        catch (NoSuchElementException)
        {
            // Element not found, proceed to next step
        }
        catch (StaleElementReferenceException)
        {
            // Element is no longer attached to the DOM, proceed to next step
        }
    }
    
    public bool IsElementDisplayed(By locator)
    {
        try
        {
            return _driver.FindElement(locator).Displayed;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
    }

    public bool IsElementNotDisplayed(By locator)
    {
        try
        {
            return _wait.Until(_driver => this._driver.FindElement(locator).GetAttribute("style").Contains("display: none"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void WaitForPageLoad()
    {
        _wait.Until(driver => (IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
    }

    public void JsClick(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = _driver as IJavaScriptExecutor;
        js.ExecuteScript("arguments[0].click();", element);
    }

    public void ScrollIntoView(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = _driver as IJavaScriptExecutor;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }
    
    public void AcceptAlert()
    {
        _driver.SwitchTo().Alert().Accept();
    }

    public void SwitchToModalTo(string id)
    {
        _driver.SwitchTo().Frame(id);
    }

    public void SwitchChromeWindowTo()
    {
        var windowHandle = _driver.WindowHandles;
        _driver.SwitchTo().Window(windowHandle[1]);
    }
}