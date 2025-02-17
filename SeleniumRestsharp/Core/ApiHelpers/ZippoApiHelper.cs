using System.Net;
using Newtonsoft.Json;
using RestSharp;
using SeleniumAndRestSharp.Core.Entity;

namespace SeleniumAndRestSharp.Core.ApiHelpers;

public class ZippoApiHelper
{
    private readonly RestClient _restClient;
    
    public ZippoApiHelper()
    {
        _restClient = new RestClient(Config.ZippoApiUrl);
    }

    public async Task<ZippoGetModel> GetZippo(string code)
    {
        return await GetAsync<ZippoGetModel>($"/us/{code}");
    }

    private async Task<T> GetAsync<T>(string url)
    {
        var request = new RestRequest(url, Method.Get);
        request = AuthenticateRequest(request);
        
        var response = await _restClient.ExecuteAsync<T>(request);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"GetAsync failed with status code {response.StatusCode}");
        }

        return JsonConvert.DeserializeObject<T>(response.Content);
    }

    private RestRequest AuthenticateRequest(RestRequest request)
    {
        if (Config.ApiKey == null)
        {
            throw new Exception("Enter the API key. Config.Apikey is null.'");
        }
        
        request.AddHeader("X-API-Key", Config.ApiKey);
        return request;
    }
}