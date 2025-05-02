using Microsoft.Playwright;

namespace Playwright.Core.ApiHelpers.Services;

public class ZippoService
{
    private readonly IAPIRequestContext _requestContext;

    public ZippoService(IAPIRequestContext requestContext)
    {
        _requestContext = requestContext;
    }

    public async Task<IAPIResponse> GetZippoAsync(string code)
    {
        return await _requestContext.GetAsync($"/us/{code}");
    }
}