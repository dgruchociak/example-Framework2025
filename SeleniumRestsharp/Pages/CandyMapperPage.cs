using OpenQA.Selenium;
using SeleniumRestsharp.Core;

namespace SeleniumRestsharp.Pages;

public class CandyMapperPage(Browser browser) : BasePage(browser)
{
    private By _loader = By.XPath("//div[@id='loader']");
    private By _map = By.Id("myDiv");
    private string _iframeId = "iframe-07";

    public bool IsMapLoaded()
    {
        return Browser.IsElementDisplayed(_map);
    }

    public CandyMapperPage WaitForLoader()
    {
        Browser.SwitchToModalTo(_iframeId);
        Browser.IsElementNotDisplayed(_loader);
        
        return this;
    }
        
    public override bool IsDisplayed()
    {
        throw new NotImplementedException();
    }

    public override T? AcceptCookies<T>() where T : class
    {
        throw new NotImplementedException();
    }
}