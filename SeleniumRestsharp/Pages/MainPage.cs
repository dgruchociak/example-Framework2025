using OpenQA.Selenium;
using SeleniumAndRestSharp.Core;

namespace SeleniumAndRestSharp.Pages;

public class MainPage(Browser browser) : BasePage(browser)
{
    private readonly By _acceptCookies = By.Id("bnp_btn_accept");
    private readonly By _searchInput = By.Id("sb_form_q");
    
    private readonly By _emailInput = By.Name("loginfmt");
    private readonly By _passwordInput = By.Name("passwd");
    private readonly By _signInButton = By.XPath("//*[@value='Sign in']");
    private readonly By _loginUsingPrivateAccount = By.ClassName("id_text_signin");
    private readonly By _loggedUserButton = By.Id("id_l");
    private readonly By _signOutButton = By.ClassName("id_signout");
    private readonly By _signOutButtonPopUp = By.Id("b_signout");
    private readonly By _errorMessage = By.Id("idTD_Error");

    public void Search(string text)
    {
        IsDisplayed();
        Browser.SendKeys(_searchInput, text);
        Browser.SendKeys(_searchInput, Keys.Enter);
    }

    public void Login(string username, string password)
    {
        IsDisplayed();
        
        if (Browser.IsElementDisplayed(_loggedUserButton))
        {
            Browser.Click(_loggedUserButton);
            if (Browser.IsElementDisplayed(_signOutButton))
            {
                Browser.Click(_signOutButton);
                Browser.Click(_signOutButtonPopUp);
            }
        }

        if (Browser.IsElementDisplayed(_signInButton))
        {
            Browser.Click(_signInButton);
        }
        Browser.Click(_loginUsingPrivateAccount);
        Browser.SendKeys(_emailInput, username);
        Browser.SendKeys(_emailInput, Keys.Enter);
        Browser.SendKeys(_passwordInput, password);
        Browser.SendKeys(_passwordInput, Keys.Enter);
    }

    public bool IsSignInBlockErrorDisplayed()
    {
        return Browser.FindElement(_errorMessage).Displayed;
    }
    
    public override T? AcceptCookies<T>() where T : class
    {
        if (Browser.WaitForElement(_acceptCookies).Displayed)
        {
            Browser.JsClick(_acceptCookies);
        }
        
        return this as T;
    }

    public override bool IsDisplayed() => Browser.FindElement(_searchInput).Displayed;

}