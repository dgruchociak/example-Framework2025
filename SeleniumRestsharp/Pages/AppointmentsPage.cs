using OpenQA.Selenium;
using SeleniumRestsharp.Core;

namespace SeleniumRestsharp.Pages;

public class AppointmentsPage(Browser browser) : BasePage(browser)
{
    private readonly By _typeOfAppointmentsList = By.XPath("//li[@data-ux='NavHorizontalListItem']//span[contains(@data-aid,'CATEGORY_LINK')]");
    private readonly By _bookButton = By.XPath("//button[contains(text(), 'BOOK')]");
    private readonly By _availableDays = By.XPath("//div[not(contains(@class, 'calendar-disabled'))]");
    private readonly By _hourButton = By.XPath("//p[@data-aid='AVAILABLE_TIMES_PERIOD_TIME']");
    private readonly By _captionCalendar = By.XPath("//div[contains(@class, 'calendar-caption')]");
    
    public AppointmentsPage ClickRandomAppointment()
    {
        var element = GetRandomDisplayedWebElement(_typeOfAppointmentsList);
        Browser.Click(element);

        return this;
    }

    public AppointmentsPage ClickBookButton()
    {
        var element = GetRandomDisplayedWebElement(_bookButton);
        Browser.Click(element);

        return this;
    }

    // public AppointmentsPage PickDateFromCalendar(string date)
    // {
    //     var formattedDate = date.Split()
    //
    //     return this;
    // }
    
    
    public override bool IsDisplayed()
    {
        throw new NotImplementedException();
    }

    public override T? AcceptCookies<T>() where T : class
    {
        throw new NotImplementedException();
    }
}