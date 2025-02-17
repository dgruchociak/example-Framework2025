using NUnit.Framework;
using SeleniumRestsharp.Core.ApiHelpers;

namespace SeleniumRestsharp.Tests.ApiTests;

public class ApiTestsExamples
{
    private ZippoApiHelper _zippoApiHelper;
    private CoinpaprikaApiHelper _coinpaprikaApiHelper;
    
    [SetUp]
    public Task Setup()
    {
        _zippoApiHelper = new ZippoApiHelper();
        _coinpaprikaApiHelper = new CoinpaprikaApiHelper();
        
        return Task.CompletedTask;
    }

    [TestCase("90210")]
    public async Task TestGet(string code)
    {
        const string country = "United States";
        var data = await _zippoApiHelper.GetZippo(code);
        
        Assert.That(data.country, Is.EqualTo(country));
    }
    
    [Test]
    public async Task TestCoinPaprikaApi()
    {
        var bitcoin = await _coinpaprikaApiHelper.GetBitcoinCoinInfoAsync();
        var allCoins = await _coinpaprikaApiHelper.GetAllCoinsAsync();
        
        Assert.That(bitcoin, Is.Not.Null);
        Assert.That(bitcoin.FirstOrDefault().Name, Is.EqualTo("Bitcoin"));
        Assert.That(bitcoin.FirstOrDefault().Id, Is.EqualTo(allCoins.Value.FirstOrDefault().Id));
    }

    [TearDown]
    public Task TearDown()
    {
        return Task.CompletedTask;
    }
}