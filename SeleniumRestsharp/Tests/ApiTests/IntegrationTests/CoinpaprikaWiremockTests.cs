using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
using SeleniumRestsharp.Core.ApiHelpers;
using WiremockServer;

namespace SeleniumRestsharp.Tests.ApiTests.IntegrationTests;

public class CoinpaprikaWiremockTests
{
    [TestFixture]
    public class CoinpaprikaWiremockFixture
    {
        private WireMock.Server.WireMockServer _wireMockServerManager;
        private CoinpaprikaApiHelper _coinpaprikaApiHelper;
        
        [SetUp]
        public async Task Setup()
        {
            //Arrange
            _wireMockServerManager = await WiremockServerManager.Start();
            _coinpaprikaApiHelper = new CoinpaprikaApiHelper();
        }

        [Test]
        public async Task CoinpaprikaWiremockTest()
        {
            // Act
            var responseBtcApi = await _coinpaprikaApiHelper.GetCoinInfoAsync("btc-bitcoin");
            var responseEthApi = await _coinpaprikaApiHelper.GetCoinInfoAsync("eth-ethereum");

            // Act2
            var mockBtcResponse = await WiremockServerManager.GetCoinById("btc-bitcoin");
            var mockBitcoinData = JsonConvert.DeserializeObject<CoinpaprikaAPI.Entity.CoinInfo>(mockBtcResponse.Content);
            
            var mockEthResponse = await WiremockServerManager.GetCoinById("eth-ethereum");
            var mockEthData = JsonConvert.DeserializeObject<CoinpaprikaAPI.Entity.CoinInfo>(mockEthResponse.Content);
            
            // Assert
            Assert.That(200, Is.EqualTo((int)mockBtcResponse.StatusCode));
            //Option 1: Compare Individual Properties
            // var responseCoinpaprika = responseApi.FirstOrDefault();
            // Assert.Multiple(() =>
            // {
            //     Assert.That(responseCoinpaprika.Id, Is.EqualTo(mockBitcoinData.Id), "IDs do not match.");
            //     Assert.That(responseCoinpaprika.IsActive, Is.EqualTo(mockBitcoinData.IsActive), "IsActive flag do not match.");
            //     Assert.That(responseCoinpaprika.IsNew, Is.EqualTo(mockBitcoinData.IsNew), "IsNew flag do not match.");
            //     Assert.That(responseCoinpaprika.Name, Is.EqualTo(mockBitcoinData.Name), "Names do not match.");
            //     Assert.That(responseCoinpaprika.Rank, Is.EqualTo(mockBitcoinData.Rank), "Rank do not match.");
            //     Assert.That(responseCoinpaprika.Symbol, Is.EqualTo(mockBitcoinData.Symbol), "Symbol do not match.");
            //     Assert.That(responseCoinpaprika.Type, Is.EqualTo(mockBitcoinData.Type), "Type do not match.");
            // });
            //Option 2: Override the Equals Method in 'CoinInfo' class
            //Option 3: Use FluentAssertions
            using (new AssertionScope())
            {
                responseBtcApi.FirstOrDefault().Should().BeEquivalentTo(mockBitcoinData);
                responseEthApi.FirstOrDefault().Should().BeEquivalentTo(mockEthData);
            }
        }
        
        [TearDown]
        public async Task TearDown()
        {
            await WiremockServerManager.Stop();
        }
    }
}