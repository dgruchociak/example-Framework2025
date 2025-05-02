using Microsoft.Playwright;
using Playwright.Pages.CommitQuality.Pages;

namespace Playwright.Pages.CommitQuality.Components;

public class ProductsComponent: BasePage
{
    private const string DefaultAccordionSelector = "//div[@class='container']";
    private readonly ILocator _rootLocator;
    
    public ProductsComponent(IPage page, ILocator rootLocator)
        : base(page)
    {
        _rootLocator = rootLocator ?? throw new ArgumentNullException(nameof(rootLocator));
        Console.WriteLine($"NavigationBarComponent created with specific root locator: {rootLocator}");
    }
    
    public ProductsComponent(IPage page)
        : base(page)
    {
        _rootLocator = page.Locator(DefaultAccordionSelector);
        Console.WriteLine($"NavigationBarComponent created with default root locator: '{DefaultAccordionSelector}'");
    }
    
    private ILocator AddProductButton => _rootLocator.Locator(".add-product-button");
    private Task<IReadOnlyList<ILocator>> ProductRows => _rootLocator.Locator("//tr[contains(@data-testid, 'product-row')]").AllAsync();
    private ILocator FilterInput => _rootLocator.Locator("//input[@type='text']");
    private ILocator FilterButton => _rootLocator.Locator("//button[@data-testid='filter-button']");
    private ILocator ShowMoreButton => _rootLocator.Locator("//button[@data-testid='show-more-button']");
    
    public async Task<AddProductPage> ClickAddProductButton()
    {
        await AddProductButton.ClickAsync();

        return new AddProductPage(Page);
    }

    public async Task<List<Dictionary<string, string>>> GetProductsList()
    {
        var productsList = new List<Dictionary<string, string>>();
        var productRows = await ProductRows;
        
        foreach (var row in productRows)
        {
            var product = new Dictionary<string, string?>
            {
                ["id"] = await row.Locator("//td[@data-testid='id']").TextContentAsync(),
                ["name"] = await row.Locator("//td[@data-testid='name']").TextContentAsync(),
                ["price"] = await row.Locator("//td[@data-testid='price']").TextContentAsync(),
                ["dateStocked"] = await row.Locator("//td[@data-testid='dateStocked']").TextContentAsync(),
            };
            
            productsList.Add(product!);
        }

        return productsList;
    }

    public async Task FilterProductsByName(string name)
    {
        await FilterInput.FillAsync(name);
        await FilterButton.ClickAsync();
    }

    public async Task ClickShowMoreButton()
    {
        await ShowMoreButton.ClickAsync();
    }
    
    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }
}