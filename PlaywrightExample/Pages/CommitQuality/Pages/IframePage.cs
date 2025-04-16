using Microsoft.Playwright;
using Playwright.Pages.CommitQuality.Components;

namespace Playwright.Pages.CommitQuality.Pages;

public class IframePage(IPage page) : BasePage(page)
{
    // OpenIframeComponent - practice page
    private ILocator IframePageComponent => Page.Locator("//div[@data-testid='practice-iframe']");

    // Iframe locator
    private IFrameLocator Frame => Page.FrameLocator("//iframe[@data-testid='iframe']");
    
    // Components inside IFrame
    private ILocator NavBarInFrame => Frame.Locator("//nav");
    private ILocator GeneralComponentInFrame => Frame.Locator("//div[@class='container']");

    // Properties in IFrame
    private NavigationBarComponent NavigationBar => new(page, NavBarInFrame);
    private GeneralComponentsComponent GeneralComponents => new(page, GeneralComponentInFrame);
    
    public async Task OpenIframePage()
    {
        await GoTo(IframePageComponent);
    }
    public async Task NavigateInFrame()
    {
        await NavigationBar.GoToPractice();
    }

    public async Task FillButtonGeneralComponentActions()
    {
        await GeneralComponents.OpenGeneralComponentsPage();
        await GeneralComponents.GeneralComponentsButtonExercise();
    }
    
    public override Task WaitForPageLoadAsync()
    {
        throw new NotImplementedException();
    }
    
}