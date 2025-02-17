using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAndRestSharp.Core;
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

    public void GoTo(string url) => _driver.Navigate().GoToUrl(url);

    public IWebElement FindElement(By locator) => _wait.Until(d => d.FindElement(locator));

    public void Click(By locator) => FindElement(locator).Click();

    public void JsClick(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = _driver as IJavaScriptExecutor;
        js.ExecuteScript("arguments[0].click();", element);
    }
    
    public IWebElement WaitForElement(By locator)
    {
        return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }

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
    
    public void WaitForPageLoad(IWebDriver driver, int timeoutInSeconds = 30)
    {
        _wait.Until(driver => (IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
    }

    public void SendKeys(By locator, string text) => FindElement(locator).SendKeys(text);

    public string GetText(By locator) => FindElement(locator).Text;
    
    public string GetTitle() => _driver.Title;
}