using NUnit.Framework;
using Playwright.Pages.CommitQuality.Components;
using Playwright.Pages.CommitQuality.Pages;

namespace Playwright.Tests.UITests.CommitQuality;

public class PracticeTests : BrowserTestFixture
{
    private NavigationBarComponent _navigationBar;
    private AccordionsComponent _accordionsComponent;
    private GeneralComponentsComponent _generalComponentsComponent;

    // private PracticePage _practicePage;
    private IframePage _iframePage;
    private readonly List<string> _buttonTextsMsg = ["Button clicked", "Button double clicked", "Button right mouse clicked"];

    [SetUp]
    public new async Task SetUp()
    {
        _navigationBar = new NavigationBarComponent(Page);
        _accordionsComponent = new AccordionsComponent(Page);
        _generalComponentsComponent = new GeneralComponentsComponent(Page);

        // _practicePage = new PracticePage(Page);
        _iframePage = new IframePage(Page);

        // await _practicePage.WaitForPageLoadAsync();
        await _navigationBar.GoToPractice();
    }

    [Test]
    public async Task GeneralComponents_01()
    {
        await _generalComponentsComponent.OpenGeneralComponentsPage();
        var buttonTexts = await _generalComponentsComponent.GeneralComponentsButtonExercise();
        Assert.That(buttonTexts, Is.EquivalentTo(_buttonTextsMsg));
        await _generalComponentsComponent.GeneralComponentsRadioButtonExercise();
        await _generalComponentsComponent.GeneralComponentsSelectOptionExercise();
        await _generalComponentsComponent.GeneralComponentsCheckBoxesExercise();
        //TODO fix cookies page youtube
        // await _generalComponentsComponent.GeneralComponentsLinksExercise();
    }

    [Test]
    public async Task AccordionsAndPopUp_02()
    {
        await _accordionsComponent.OpenAccordionsPage();
        await _accordionsComponent.ClickRandomAccordion();
    }
    
    [Test]
    public async Task PopUps_03()
    {
        await _accordionsComponent.OpenPopupsPage();
        await _accordionsComponent.ClickRandomAccordion();
    }
    
    [Test]
    public async Task Iframes_04()
    {
        await _iframePage.OpenIframePage();
        await _iframePage.NavigateInFrame();
        await _iframePage.FillButtonGeneralComponentActions();
    }
    
    [Test]
    public async Task Apis_05()
    {
        
    }
    
    [Test]
    public async Task Dynamic_text_06()
    {
        
    }
    
    [Test]
    public async Task FileUpload_07()
    {
        
    }
    
    [Test]
    public async Task DragAndDrop_08()
    {
        
    }
    
    [Test]
    public async Task ContactUsForm_09()
    {
        
    }
    
    [Test]
    public async Task Mock_Datalayer_10()
    {
        
    }
    
    [Test]
    public async Task FileDownload_11()
    {
        
    }
    
    [Test]
    public async Task TimeTesting_12()
    {
        
    }
}