using Microsoft.Extensions.Configuration;
using Playwright.Core.Entity;

namespace Playwright.Core.Configuration;

public static class TestConfiguration
{
    private static readonly IConfigurationRoot Configuration;

    static TestConfiguration()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static ApiSettingsDto GetApiSettings()
    {
        var settings = new ApiSettingsDto();
        Configuration.GetSection("ApiSettings").Bind(settings);
        return settings;
    }
}