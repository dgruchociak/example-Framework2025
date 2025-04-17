using Microsoft.Playwright;

namespace Playwright.Pages.CommitQuality.Pages;

public class FilePage(IPage page) : BasePage(page)
{
    private const string FilePath = "C:\\Users\\Dell\\RiderProjects\\example-Framework2025\\PlaywrightExample\\testFile.txt";

    private ILocator FileUploadComponent => Page.Locator("//div[@data-testid='practice-file-upload']");
    private ILocator FileDownloadComponent => Page.Locator("//div[@data-testid='practice-file-download']");
    private ILocator ChooseFileInput => Page.Locator("//input[@data-testid='file-input']");
    private ILocator DownloadFileButton => Page.GetByText("Download File");
    private ILocator SubmitButton => Page.GetByText("Submit");
        
    public async Task OpenFileUploadPage()
    {
        await FileUploadComponent.ClickAsync();
    }
    
    public async Task OpenFileDownloadPage()
    {
        await FileDownloadComponent.ClickAsync();
    }

    public async Task UploadFile()
    {
        await Page.PauseAsync();
        await ChooseFileInput.SetInputFilesAsync(FilePath);
        
        Page.Dialog += async (_, dialog) =>
        {
            await Page.PauseAsync();
            await dialog.AcceptAsync();
        };

        await SubmitButton.ClickAsync();
    }

    public async Task DownloadFile()
    {
        var waitForDownloadTask = Page.WaitForDownloadAsync();
        await DownloadFileButton.ClickAsync();
        var download = await waitForDownloadTask;

        await download.SaveAsAsync("./../../../" + download.SuggestedFilename);
    }
    
    public override Task WaitForPageLoadAsync()
    {
        throw new NotImplementedException();
    }
}