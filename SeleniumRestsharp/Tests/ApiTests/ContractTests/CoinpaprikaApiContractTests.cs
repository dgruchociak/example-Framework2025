// using NUnit.Framework;
// using PactNet;
// using System.Net;
// using System.Text.Json;
//TODO add contract testing
//
// [TestFixture]
// public class CoinpaprikaApiContractTests
// {
//     private IPactBuilderV4 _pactBuilder;
//     private string _mockProviderBaseUri;
//
//     [SetUp]
//     public void Setup()
//     {
//         // Initialize Pact Builder with Consumer and Provider names
//         _pactBuilder = Pact.V4("MyApplication", "CoinpaprikaAPI", new PactConfig
//         {
//             PactDir = @"..\..\..\pacts", // Directory to save the generated pact file
//             LogDir = @".\pact_logs" // Directory for logs
//         });
//     }
//
//     [Test]
//     public async Task GetBitcoinInfo_WhenCalled_ReturnsCorrectResponse()
//     {
//         // Arrange: Define the expected interaction
//         var expectedResponse = new
//         {
//             id = "btc-bitcoin",
//             name = "Bitcoin",
//             symbol = "BTC",
//             rank = 1,
//             is_new = false,
//             is_active = true,
//             type = "coin"
//         };
//
//         _pactBuilder
//             .UponReceiving("A GET request to retrieve Bitcoin info")
//             .Given("Bitcoin info exists")
//             .WithRequest(HttpMethod.Get, "/v1/coins/btc-bitcoin")
//             .WithHeader("Accept", "application/json")
//             .WillRespond()
//             .WithStatus(HttpStatusCode.OK)
//             .WithHeader("Content-Type", "application/json; charset=utf-8")
//             .WithJsonBody(expectedResponse);
//
//         // Act: Verify the interaction and test your consumer logic
//         await _pactBuilder.VerifyAsync(async ctx =>
//         {
//             _mockProviderBaseUri = ctx.MockServerUri.ToString(); // Get the mock server URI
//
//             var httpClient = new HttpClient { BaseAddress = new System.Uri(_mockProviderBaseUri) };
//             var response = await httpClient.GetAsync("/v1/coins/btc-bitcoin");
//
//             // Assert.That(HttpStatusCode.OK, response.StatusCode);
//
//             var content = await response.Content.ReadAsStringAsync();
//             var actualResponse = JsonSerializer.Deserialize<dynamic>(content);
//
//             // Assert.AreEqual(expectedResponse.id, actualResponse.id.ToString());
//             // Assert.AreEqual(expectedResponse.name, actualResponse.name.ToString());
//         });
//     }
// }
