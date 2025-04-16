using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core.Helpers;

namespace Playwright.Pages.CommitQuality.Components;

public class GeneralComponentsComponent : BasePage
{
    private const string DefaultGeneralComponentsSelector = "//div[@class='container']";
    private readonly ILocator _rootLocator;
    
    public GeneralComponentsComponent(IPage page, ILocator rootLocator)
        : base(page)
    {
        _rootLocator = rootLocator ?? throw new ArgumentNullException(nameof(rootLocator));
        Console.WriteLine($"NavigationBarComponent created with specific root locator: {rootLocator}");
    }

    public GeneralComponentsComponent(IPage page)
        : base(page)
    {
        _rootLocator = page.Locator(DefaultGeneralComponentsSelector);
        Console.WriteLine($"NavigationBarComponent created with default root locator: '{DefaultGeneralComponentsSelector}'");
    }
    
    private ILocator GeneralComponents => _rootLocator.Locator("//div[@data-testid='practice-general']");

    //general-components-buttons
    private ILocator BasicClickButton => _rootLocator.Locator("//button[@data-testid='basic-click']");
    private ILocator DoubleClickButton => _rootLocator.Locator("//button[@data-testid='double-click']");
    private ILocator RightClickButton => _rootLocator.Locator("//button[@data-testid='right-click']");
    private ILocator ButtonParagraphText => _rootLocator.Locator("//div[@class='button-container']/p");
    
    //general-components-radio buttons
    private ILocator RadioButton1 => _rootLocator.Locator("//input[@data-testid='option1']");
    private ILocator RadioButton2 => _rootLocator.Locator("//input[@data-testid='option2']");
    private ILocator RadioParagraphText => _rootLocator.Locator("//div[@class='component-container']/p");
    
    //general-components-select
    private ILocator SelectOption => _rootLocator.Locator("//div[@class='dropdowns']/select");
    
    //general-components-checkbox
    private ILocator CheckboxContainer => _rootLocator.Locator("//div[@class='checkbox-container']/input");
    private ILocator CheckboxParagraphText => _rootLocator.Locator("//div[@class='checkbox-container']/p");
    
    //general-components-links
    private ILocator YoutubeLink => _rootLocator.Locator("//a[@data-testid='link-newtab']");
    private ILocator ZaakceptujCookiesButton => _rootLocator.Locator("//span[contains(text(),\"Zaakceptuj wszystko\")]");
    
    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }

    public async Task OpenGeneralComponentsPage()
    {
        await GoTo(GeneralComponents);
    }
    
    public async Task<IReadOnlyList<string>> GeneralComponentsButtonExercise()
    {
        await BasicClickButton.ClickAsync();
        await DoubleClickButton.DblClickAsync();
        await RightClickButton.ClickAsync(new LocatorClickOptions()
        {
            Button = MouseButton.Right
        });

        var pTexts = await GetButtonParagraphTexts(ButtonParagraphText);
        
        return pTexts;
    }

    public async Task GeneralComponentsRadioButtonExercise()
    {
        await RadioButton1.CheckAsync();
        var pText = await GetButtonParagraphTexts(RadioParagraphText);
        Assert.That(pText[0], Is.EqualTo("option1 clicked"));
        await RadioButton2.CheckAsync();
        var pText2 = await GetButtonParagraphTexts(RadioParagraphText);
        Assert.That(pText2[0], Is.EqualTo("option2 clicked"));
    }

    public async Task GeneralComponentsSelectOptionExercise()
    {
        await SelectOption.SelectOptionAsync("option1");
    }

    public async Task GeneralComponentsCheckBoxesExercise()
    {
        var checkboxes = await CheckboxContainer.AllAsync();
        var checkboxName = await TestHelper.GetRandomElement(checkboxes).GetAttributeAsync("name");
        if (checkboxName != null)
        {
            var checkboxLocator = Page.Locator($"//input[@name='{checkboxName}']");
            await checkboxLocator.CheckAsync();
            await Assertions.Expect(checkboxLocator).ToBeCheckedAsync();

        }

        var paragraphTexts = await GetButtonParagraphTexts(CheckboxParagraphText);
        var checkboxLabel = await Page.Locator($"//input[@name='{checkboxName}']/following-sibling::label").InnerTextAsync();
        Assert.That(paragraphTexts[0], Is.EqualTo($"{checkboxLabel} checked"));
    }
    
    public async Task GeneralComponentsLinksExercise()
    {
        var newPage = await Page.RunAndWaitForPopupAsync(async () =>
        {
            await YoutubeLink.ClickAsync();
        });
        
        await ZaakceptujCookiesButton.ClickAsync();
        await newPage.WaitForLoadStateAsync();
        
        Assert.That(newPage.TitleAsync(), Is.EqualTo("CommitQuality - Youtube"));
    }

    private static async Task<IReadOnlyList<string>> GetButtonParagraphTexts(ILocator locator)
    {
        return await locator.AllTextContentsAsync();
    }
}