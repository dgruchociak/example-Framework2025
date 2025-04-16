using Microsoft.Playwright;

namespace Playwright.Pages.Bing;

public class SearchResultsPage(IPage page) : BasePage(page)
{
    private ILocator ResultsList => Page.Locator("xpath=//ol[@id='b_results']/li/h2");

    public override async Task WaitForPageLoadAsync()
    {
        var elements = ResultsList.AllAsync();
        await Page.WaitForLoadStateAsync();
        await elements.WaitAsync(TimeSpan.FromSeconds(5));
    }

    public async Task<int> GetResultsCountAsync()
    {
        return await ResultsList.CountAsync();
    }
}