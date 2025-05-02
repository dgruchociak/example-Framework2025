namespace Playwright.Core.Entity;

public record ApiSettingsDto
{
    public string BaseUrl { get; set; }
    public Dictionary<string, string>? DefaultHeaders { get; set; }
}