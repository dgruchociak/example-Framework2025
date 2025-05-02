using Microsoft.Playwright;
using NUnit.Framework.Internal;
using Playwright.Core.Configuration;
using Playwright.Core.Entity;

namespace Playwright.Core.ApiHelpers;

public class ApiClient : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IAPIRequestContext? _requestContext;
    private readonly ApiSettingsDto _settings;

    public ApiClient()
    {
        _settings = TestConfiguration.GetApiSettings();
    }

    public async Task<IAPIRequestContext> GetRequestContextAsync()
    {
        if (_requestContext == null)
        {
            _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var options = new APIRequestNewContextOptions()
            {
                BaseURL = _settings.BaseUrl,
                ExtraHTTPHeaders = _settings.DefaultHeaders
            };
            _requestContext = await _playwright.APIRequest.NewContextAsync(options);
        }
        return _requestContext;
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_requestContext != null)
        {
            await _requestContext.DisposeAsync();
        }
        _playwright?.Dispose();
        GC.SuppressFinalize(this);
    }
}