using NUnit.Framework;
using SeleniumRestsharp.Pages;

namespace SeleniumRestsharp.Tests.UITests;

public class CandyTests : BaseTest
{
    private CandyMainPage _candyMainPage;

    [SetUp]
    public void Setup()
    {
        _candyMainPage = new CandyMainPage(Browser);
    }
    
    [Test]
    public void CheckEmailValidationAndFillForm()
    {
        _candyMainPage.AcceptModalPopUp();
        Browser.WaitForPageLoad();
        Assert.That(_candyMainPage.ClickGetInTouchButton()
            .FillEmail("test")
            // .ClickSendButton()
            .IsErrorMsgDisplayed());
        Assert.That(_candyMainPage.FillName("testName")
            .FillMessage("testMessageee")
            .FillEmail("test@mail.com")
            .ClickSendButton()
            .IsSuccessMsgDisplayed());
    }

    [Test]
    public void CheckIfLoaderIsGoneAndMapLoaded()
    {
        _candyMainPage.AcceptModalPopUp();
        Assert.That(_candyMainPage.OpenLaunchCandyMapperPage()
            .WaitForLoader()
            .IsMapLoaded());
    }

    [Test]
    public void CheckIfNewPageIsOpen()
    {
        _candyMainPage.AcceptModalPopUp();
        var title = Browser.GetTitle();
        _candyMainPage.OpenPacktPublishingPage();
        Browser.SwitchChromeWindowTo();
        Assert.That(title, Is.Not.EqualTo(Browser.GetTitle()));
    }

    [Test]
    public void BookAppointmentAndConfirm()
    {
        _candyMainPage.AcceptModalPopUp()
            .OpenAppointmentsPage()
            .ClickRandomAppointment()
            .ClickBookButton();
        // .PickAvailableDateFromCalendar();
    }
}