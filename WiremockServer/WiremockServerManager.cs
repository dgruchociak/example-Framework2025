using RestSharp;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace WiremockServer;

public class WiremockServerManager
{
    private static WireMockServer _server;
        
    // 1. Benefits of Making It Async:
    //     Non-Blocking Execution: If starting or stopping the WireMock server involves I/O operations (e.g., file reads or network calls), making it asynchronous ensures that your test runner’s thread isn’t blocked.
    //     Scalability: If you run multiple tests in parallel that each start their own instance of the WireMock server, async methods can help reduce thread usage and improve performance.
    //     Consistency: Many modern APIs in .NET are asynchronous by default (e.g., HttpClient, file I/O). Making your code async aligns with this pattern.
    // 2. When Not to Make It Async:
    //     If starting and stopping the WireMock server are purely synchronous operations (e.g., in-memory setup with no external dependencies), making these methods async adds unnecessary complexity without any real benefit.

    // But for educational purpose I've made it async ;)

    public static Task<WireMockServer> Start()
    {
        if (_server == null)
        {
            // TODO can be moved to appsettings.json
            var settings = new WireMockServerSettings
            {
                Port = 9095,
                UseSSL = false,
                StartAdminInterface = true,
                ReadStaticMappings = true, // Enable reading mappings from files
            };

            _server = WireMockServer.Start(settings);
            
            // TODO instead of Console.WriteLine implement logging
            Console.WriteLine($"WireMock.NET server is running on ({_server.Urls[0]})");

            // Define a stub mappings:
            // Option 1 is to define mapping like below and other better options is to use 'mappings/*.json'
            _server
                .Given(Request.Create().WithPath("/v1/coins/btc-bitcoin").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(@"{""id"": ""btc-bitcoin"", ""isActive"": true, ""isNew"": false, ""name"": ""Bitcoin"", ""rank"": 1, ""symbol"": ""BTC"", ""type"": ""coin""}"));
        }
        return Task.FromResult(_server);
    }

    public static Task Stop()
    {
        if (_server != null)
        {
            Console.WriteLine("Stopping WireMock server...");
            _server.Stop();
            _server = null;
        }
            
        return Task.CompletedTask;
    }

    public static async Task<RestResponse> GetCoinById(string id)
    {
        var mockClient = new RestClient("http://localhost:9095");
        var mockRequest = new RestRequest($"/v1/coins/{id}");
        var mockResponse = await mockClient.ExecuteAsync(mockRequest);

        return mockResponse;
    }
}