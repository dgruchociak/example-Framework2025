using Microsoft.Playwright;

namespace Playwright.Pages;

public abstract class BasePage(IPage page)
{
    protected readonly IPage Page = page ?? throw new ArgumentNullException(nameof(page));

    public abstract Task WaitForPageLoadAsync();
    
    protected async Task WaitForButtonIsEnabled(ILocator selector)
    {
        var startTime = DateTime.UtcNow;

        while (await selector.EvaluateAsync<bool>("el => el.disabled"))
        {
            if ((DateTime.UtcNow - startTime) > TimeSpan.FromSeconds(2))
            {
                throw new TimeoutException("Submit button did not become enabled within the timeout period.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
    
    protected async Task GoTo(ILocator locator)
    {
        await locator.ClickAsync();
    }
}