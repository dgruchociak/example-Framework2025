using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core.ApiHelpers;
using Playwright.Core.ApiHelpers.Services;
using Playwright.Core.Entity;

namespace Playwright.Tests.ApiTests;

[TestFixture]
public class ZippoApiTests : IAsyncDisposable
{
    private ApiClient _apiClient;
    private IAPIRequestContext _requestContext;
    private ZippoService _zippoService;
    
    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _apiClient = new ApiClient();
        _requestContext = await _apiClient.GetRequestContextAsync();
        _zippoService = new ZippoService(_requestContext);
    }
    
    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await DisposeAsync();
    }

    [Test]
    public async Task GetCountryByCode_Should_Return_CountryData()
    {
        var code = "90210";
        //ACT
        var response = await _zippoService.GetZippoAsync(code);

        //ASSERT
        using (new AssertionScope())
        {
            response.Ok.Should().BeTrue($"API should return success for country {code}");
            response.Status.Should().Be((int) HttpStatusCode.OK);
            var country = await response.JsonAsync<ZippoGetModel>();
            country?.country.Should().Be("United States");
        }
    }

    // [Test]
    // public async Task GetCountryByCode_Should_Return_CountryDataBuilderExample()
    // {
    //     //ARRANGE
    //     var newRequest = new CreateRequestBuilder()
    //         .WithHeader() etc.
    //         .WithCountry("United States")
    //         .WithPostCode("90210")
    //         .Build();
    //     
    //     //ACT 
    //     // var response = await _createZippoCountry(newRequest); 
    //          OR
    // private var response = await _restFacory()
    //     .Create()
    //     .WithRequest()
    //     .WithHeader()
    //     .WithGet();
    
    //     
    //     //Assert
    // }
    
    public async ValueTask DisposeAsync()
    {
        if (_apiClient != null)
        {
            await _apiClient.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}