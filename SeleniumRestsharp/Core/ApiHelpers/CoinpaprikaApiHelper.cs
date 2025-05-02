using CoinpaprikaAPI.Entity;
using CoinpaprikaAPI.Models;

namespace SeleniumRestsharp.Core.ApiHelpers;

public class CoinpaprikaApiHelper
{
    private readonly CoinpaprikaAPI.Client _client = new();

    public async Task<IEnumerable<CoinInfo>> GetCoinInfoAsync(string id)
    {
        var allCoins = await _client.GetCoinsAsync();
        var bitcoin = allCoins.Value.Where(x => x.Id == id);
        
        return bitcoin;
    }

    public async Task<CoinPaprikaEntity<List<CoinInfo>>> GetAllCoinsAsync()
    {
        return await _client.GetCoinsAsync();
    }
}