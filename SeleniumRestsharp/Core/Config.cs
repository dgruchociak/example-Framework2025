namespace SeleniumRestsharp.Core;

// TODO can be moved to appsettings.json
public static class Config
{
    public static string BaseUrl => "https://www.bing.com";
    public static string CandyUrl => "https://candymapperr2.com/";
    public static string ZippoApiUrl => "http://api.zippopotam.us";
    public static string StockMarketApiUrl => "http://localhost:9095/";
    public static string ApiKey => "APIKey";
    public static string Browser => "chrome";
    public static int DefaultTimeout => 40;
    public static bool Headless => false;
}