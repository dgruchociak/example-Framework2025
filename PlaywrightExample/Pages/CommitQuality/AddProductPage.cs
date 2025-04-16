using Microsoft.Playwright;
using Playwright.Core.Helpers;

namespace Playwright.Pages.CommitQuality;

public class AddProductPage(IPage page) : BasePage(page)
{
    private ILocator NameInput => Page.Locator("#name");
    private ILocator PriceInput => Page.Locator("#price");
    private ILocator SubmitButton => Page.Locator(".btn-primary");
    
    public async Task<string> SubmitNewProduct()
    {
        var name = TestHelper.GenerateRandomString();
        await EnterName(name);
        await EnterPrice("100");
        //TODO fix enterDate - js not working properly after submit
        await EnterDate("2020-01-01");
        await ClickSubmit();

        return name;
    }
    
    private async Task EnterName(string name)
    {
        await NameInput.FillAsync(name);
    }

    private async Task EnterPrice(string price)
    {
        await PriceInput.FillAsync(price);

    }

    private async Task EnterDate(string date)
    {
        await Page.WaitForLoadStateAsync();
        Thread.Sleep(3000);
        await Page.EvalOnSelectorAsync("#dateStocked", $"el => el.value = '{date}'");
    }

    private async Task ClickSubmit()
    {
        await SubmitButton.ClickAsync();
    }
    
    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }
}