using Microsoft.Playwright;
using Playwright.Core.Helpers;

namespace Playwright.Pages.CommitQuality.Components;

public class AccordionsComponent : BasePage
{
    private const string DefaultAccordionSelector = "//div[@class='container']";
    private readonly ILocator _rootLocator;
    
    public AccordionsComponent(IPage page, ILocator rootLocator)
        : base(page)
    {
        _generalComponents = new GeneralComponentsComponent(Page);
        _rootLocator = rootLocator ?? throw new ArgumentNullException(nameof(rootLocator));
    }
    
    public AccordionsComponent(IPage page)
        : base(page)
    {
        _generalComponents = new GeneralComponentsComponent(Page);
        _rootLocator = page.Locator(DefaultAccordionSelector);
    }
    
    private readonly GeneralComponentsComponent _generalComponents;
    
    private ILocator AccordionsPop => _rootLocator.Locator("//div[@data-testid='practice-accordions']");
    private ILocator Accordions => _rootLocator.Locator("//button[@data-testid='accordion-1']");
    private ILocator ButtonContainer => _rootLocator.Locator("//div[@class='button-container']");
    private ILocator RadioContainer => _rootLocator.Locator("//div[@class='radio-button-container']");
    private ILocator CheckboxContainer => _rootLocator.Locator("//div[@class='checkbox-container']");
    private ILocator Popups => _rootLocator.Locator("//div[@data-testid='practice-random-overlay']");
    private ILocator PopUpContainer => _rootLocator.Locator("//div[@class='overlay']");
    private ILocator CloseButton => _rootLocator.Locator("//div[@class='overlay']//button");

    public override async Task WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync();
    }

    public async Task OpenAccordionsPage()
    {
        await GoTo(AccordionsPop);
    }

    public async Task OpenPopupsPage()
    {
        await GoTo(Popups);
    }

    public async Task ClosePopUp()
    {
        try
        {
            await PopUpContainer.WaitForAsync(new LocatorWaitForOptions {State = WaitForSelectorState.Visible, Timeout = 500});

            if (await PopUpContainer.IsVisibleAsync())
            {
                await CloseButton.ClickAsync(new LocatorClickOptions { Timeout = 2000 });
                
                await Assertions.Expect(PopUpContainer).ToBeHiddenAsync(new LocatorAssertionsToBeHiddenOptions { Timeout = 2000 });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task ClickRandomAccordion()
    {
        var attempt = 0;
        var maxAttempts = 3;
        
        while (true)
        {
            attempt++;
            try
            {
                var accordions = await Accordions.AllAsync();
                var accordion = TestHelper.GetRandomElement(accordions);

                await accordion.ClickAsync();

                if (await accordion.TextContentAsync() == "Accordion 1")
                {
                    await Assertions.Expect(ButtonContainer).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions {Timeout = 5000});
                    await _generalComponents.GeneralComponentsButtonExercise();
                }
                else if (await accordion.TextContentAsync() == "Accordion 2")
                {
                    await Assertions.Expect(RadioContainer).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions {Timeout = 5000});
                    await _generalComponents.GeneralComponentsRadioButtonExercise();
                }
                else if (await accordion.TextContentAsync() == "Accordion 3")
                {
                    await Assertions.Expect(CheckboxContainer).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions {Timeout = 5000});
                    await _generalComponents.GeneralComponentsCheckBoxesExercise();
                }

                return;
            }
            catch (Exception e)
            {
                if (attempt >= maxAttempts)
                {
                    throw;
                }
                //TODO zamiast tego mozna uzyc Page.AddLocatorHandlerAsync(Page.GetText(""), async () => {await Page.GetByTestId.ClickAsync();})
                try
                {
                    await CloseButton.ClickAsync(new LocatorClickOptions { Timeout = 2000 });
                }
                catch (Exception popupEx)
                {
                    Console.WriteLine($"Error while trying close popup: {popupEx.GetType().Name} - {popupEx.Message}");
                }
            }
        }
    }
}