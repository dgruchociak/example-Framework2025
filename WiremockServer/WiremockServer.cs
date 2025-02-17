using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace WiremockServer;

internal abstract class WiremockServer
{
    private static void Main(string[] args)
    {
        var server = WireMockServer.Start(new WireMockServerSettings
        {
            Port = 9095,
            UseSSL = false
        });

        Console.WriteLine($"WireMock.NET server is running on {server.Urls[0]}");

        // Define a stub
        server
            .Given(Request.Create().WithPath("/v1/coins/btc-bitcoin").UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{""id"":btc-bitcoin,""IsActive"":true,""IsNew"":false,""name"":""Bitcoin"",""Rank"":1,""Symbol"":""BTC"",""Type"":""coin""}"));
        
        
        Console.WriteLine("Press any key to stop the server...");
        Console.ReadKey();

        server.ReadStaticMappings();

        server.Stop();
    }
}