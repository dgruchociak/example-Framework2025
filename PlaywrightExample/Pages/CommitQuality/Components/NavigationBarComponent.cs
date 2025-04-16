using Microsoft.Playwright;

namespace Playwright.Pages.CommitQuality.Components;

public class NavigationBarComponent : BasePage
{
    private const string DefaultNavBarSelector = "//nav";
    private readonly ILocator _rootLocator;

    public NavigationBarComponent(IPage page, ILocator rootLocator)
        : base(page)
    {
        _rootLocator = rootLocator ?? throw new ArgumentNullException(nameof(rootLocator));
        Console.WriteLine($"NavigationBarComponent created with specific root locator: {rootLocator}");
    }
    
    public NavigationBarComponent(IPage page)
        : base(page)
    {
        _rootLocator = page.Locator(DefaultNavBarSelector);
        Console.WriteLine($"NavigationBarComponent created with default root locator: '{DefaultNavBarSelector}'");
    }
    
    private ILocator ProductsLink => _rootLocator.Locator("a:has-text('Products')");
    private ILocator AddProductLink => _rootLocator.Locator("a:has-text('Add Product')");
    private ILocator PracticeLink => _rootLocator.Locator("//a[@data-testid='navbar-practice']");
    private ILocator LearnLink => _rootLocator.Locator("a:has-text('Learn')");
    private ILocator LoginLink => _rootLocator.Locator("a:has-text('Login')");

    
    public async Task GoToProducts() => await ProductsLink.ClickAsync();
    public async Task GoToAddProduct() => await AddProductLink.ClickAsync();
    public async Task GoToPractice() => await PracticeLink.ClickAsync();
    public async Task GoToLearn() => await LearnLink.ClickAsync();
    public async Task GoToLogin() => await LoginLink.ClickAsync();
    
    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }
}