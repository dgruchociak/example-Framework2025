using Microsoft.Playwright;

namespace Playwright.Pages.CommitQuality.Components;

public class DragAndDropComponent : BasePage
{
    private readonly ILocator _rootLocator;
    private const string DefaultDragAndDropSelector = "//div[@class='container']";

    public DragAndDropComponent(IPage page, ILocator rootLocator) : base(page)
    {
        _rootLocator = rootLocator ?? throw new ArgumentNullException(nameof(rootLocator));
    }

    public DragAndDropComponent(IPage page) : base(page)
    {
        _rootLocator = page.Locator(DefaultDragAndDropSelector);
    }
    
    private ILocator SmallBox => _rootLocator.GetByTestId("small-box");
    private ILocator LargeBox => _rootLocator.GetByTestId("large-box");
    private ILocator DragAndDropPage => _rootLocator.Locator("//div[@data-testid='practice-drag-drop']");
    
    
    public async Task OpenDragAndDropPage()
    {
        await DragAndDropPage.ClickAsync();
    }
    
    public async Task DragAndDrop()
    {
        await SmallBox.DragToAsync(LargeBox);
    }

    public override Task WaitForPageLoadAsync()
    {
        throw new NotImplementedException();
    }
}