using OpenQA.Selenium;
using SeleniumRestsharp.Core;

namespace SeleniumRestsharp.Pages;

public class CandyMainPage(Browser browser) : BasePage(browser)
{
    private readonly By _getInTouchButton = By.XPath("//a[@data-aid='HEADER_CTA_BTN']");
    private readonly By _nameInput = By.XPath("//input[@data-aid='CONTACT_FORM_NAME']");
    private readonly By _emailInput = By.XPath("//input[@data-aid='CONTACT_FORM_EMAIL']");
    private readonly By _msgInput = By.XPath("//textarea[@data-aid='CONTACT_FORM_MESSAGE']");
    private readonly By _sendButton = By.XPath("//button[@data-aid='CONTACT_SUBMIT_BUTTON_REND']");
    private readonly By _successMsg = By.XPath("//div[@data-aid='CONTACT_FORM_SUBMIT_SUCCESS_MESSAGE']");
    private readonly By _errorMsg = By.XPath("//p[@data-aid='CONTACT_EMAIL_ERR_REND']");
    private readonly By _modalButton = By.XPath("//a[@data-aid='CTA_RENDERED']");
    private readonly By _launchCandyMapperHeaderButton = By.XPath("//a[contains(text(), 'Launch CandyMapper') and @data-ux='NavLink']");
    private readonly By _packtPublishingHeaderButton = By.XPath("//a[contains(text(), 'PACKT Publishing') and @data-ux='NavLink']");
    private readonly By _moreHeaderButton = By.XPath("//a[@data-aid='NAV_MORE']");
    // private readonly By _moreOptionsListHeaderDropdown = By.XPath("//li//ul[@data-ux='Dropdown']//li[@style='visibility: visible;']");
    private readonly By _appointmentsOptionDropdown = By.XPath("//li[@style='visibility: visible;']//a[contains(text(),'Appointments')]");

    public CandyMainPage ClickGetInTouchButton()
    {
        Browser.Click(_getInTouchButton);
        
        return this;
    }

    public CandyMainPage ClickSendButton()
    {
        Browser.Click(_sendButton);
        
        return this;
    }

    public CandyMainPage FillName(string name)
    {
        Browser.ScrollIntoView(_nameInput);
        Browser.SendKeys(_nameInput, name);

        return this;
    }

    public CandyMainPage FillEmail(string email)
    {
        Browser.ScrollIntoView(_emailInput);
        Browser.SendKeys(_emailInput, email);
        Browser.Click(_msgInput);
        
        return this;
    }

    public CandyMainPage FillMessage(string msg)
    {
        Browser.ScrollIntoView(_msgInput);
        Browser.SendKeys(_msgInput, msg);
        
        return this;
    }

    public CandyMapperPage OpenLaunchCandyMapperPage()
    {
        Browser.Click(_launchCandyMapperHeaderButton);

        return new CandyMapperPage(Browser);
    }

    public CandyMainPage OpenPacktPublishingPage()
    {
        Browser.Click(_packtPublishingHeaderButton);

        return this;
    }

    public AppointmentsPage OpenAppointmentsPage()
    {
        Browser.Click(_moreHeaderButton);
        Browser.Click(_appointmentsOptionDropdown);

        return new AppointmentsPage(Browser);
    }

    public bool IsSuccessMsgDisplayed()
    {
        Browser.WaitForElement(_successMsg);
        
        return Browser.IsElementDisplayed(_successMsg);
    }
    
    public bool IsErrorMsgDisplayed()
    {
        Browser.WaitForElement(_errorMsg);
        
        return Browser.IsElementDisplayed(_errorMsg);
    }

    // public void AcceptAlert()
    // {
    //     try
    //     {
    //         Browser.AcceptAlert();
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw;
    //     }
    // }

    public CandyMainPage AcceptModalPopUp()
    {
        Browser.Click(_modalButton);

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