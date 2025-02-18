using FluentAssertions;
using NUnit.Framework;
using SeleniumRestsharp.Core.ApiHelpers;

namespace SeleniumRestsharp.Tests.ApiTests.UnitTests
{
    [TestFixture]
    public class CoinpaprikaApiHelperTests
    {
        private CoinpaprikaApiHelper _apiHelper;

        [SetUp]
        public Task Setup()
        {
            _apiHelper = new CoinpaprikaApiHelper();
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetBitcoinCoinInfoAsync_ReturnsAnyData()
        {
            var result = await _apiHelper.GetCoinInfoAsync("btc-bitcoin");

            result.FirstOrDefault().Should().NotBeNull();
            result.FirstOrDefault().Should().Be(result.Any());
        }
        
        [TearDown]
        public Task TearDown()
        {
            return Task.CompletedTask;
        }
    }
}