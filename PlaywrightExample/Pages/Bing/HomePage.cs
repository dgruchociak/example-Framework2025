using Microsoft.Playwright;

namespace Playwright.Pages.Bing;

public class HomePage(IPage page) : BasePage(page)
{
    private ILocator SearchInput => Page.Locator("#sb_form_q");
    private ILocator SearchButton => Page.Locator("#search_icon");
    private ILocator AcceptCookiesButton => Page.Locator("#bnp_btn_accept");

    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }

    public async Task SearchAsync(string text)
    {
        await SearchInput.FillAsync(text);
        await SearchButton.PressAsync("Enter");
    }

    public async Task AcceptCookies()
    {
        await WaitForButtonIsEnabled(AcceptCookiesButton);
        
        await AcceptCookiesButton.IsVisibleAsync();
        {
            await AcceptCookiesButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await AcceptCookiesButton.ClickAsync(new LocatorClickOptions
            {
                Delay = 1000,
                Timeout = 5000
            });
        }
    }
}