// using Microsoft.Playwright;
//
// namespace Playwright.Pages.CommitQuality.Practice;
//
// public class PracticePage(IPage page) : BasePage(page)
// {
//     //Practice page components
//     private ILocator GeneralComponents => Page.Locator("//div[@data-testid='practice-general']");
//     private ILocator Accordions => Page.Locator("//div[@data-testid='practice-accordions']");
//     private ILocator PopUps => Page.Locator("//div[@data-testid='practice-random-overlay']");
//     private ILocator Iframes => Page.Locator("//div[@data-testid='practice-iframe']");
//     private ILocator Apis => Page.Locator("//div[@data-testid='practice-api']");
//     private ILocator DynamicText => Page.Locator("//div[@data-testid='practice-dynamic-text']");
//     
//     public override async Task WaitForPageLoadAsync()
//     {
//         await Page.WaitForLoadStateAsync();
//     }
// }