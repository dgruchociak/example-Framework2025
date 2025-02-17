using NUnit.Framework;
using SeleniumAndRestSharp.Pages;

namespace SeleniumAndRestSharp.Tests.UITests;

public class SmokeUiTests : BaseTest
{
    private MainPage _mainPage;

    [SetUp]
    public new void SetUp()
    {
        _mainPage = new MainPage(Browser);
    }

    [Test]
    public void InvalidLogin_ShouldFail()
    {
        _mainPage.Login("test@email.com", "invalidpassword");
        Assert.That(_mainPage.IsSignInBlockErrorDisplayed());
    }
    
    [Test]
    public void ValidSearch_ShouldSucceed()
    {
        var title = Browser.GetTitle();
        _mainPage.Search("trading");
        Assert.That(title, Is.Not.EqualTo(Browser.GetTitle()));
    }
}