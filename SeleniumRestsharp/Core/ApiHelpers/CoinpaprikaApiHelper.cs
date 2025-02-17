using CoinpaprikaAPI.Entity;
using CoinpaprikaAPI.Models;

namespace SeleniumAndRestSharp.Core.ApiHelpers;

public class CoinpaprikaApiHelper
{
    private CoinpaprikaAPI.Client _client;

    public CoinpaprikaApiHelper()
    {
        _client = new CoinpaprikaAPI.Client();
    }

    public async Task<IEnumerable<CoinInfo>> GetBitcoinCoinInfoAsync()
    {
        var allCoins = await _client.GetCoinsAsync();
        var bitcoin = allCoins.Value.Where(x => x.Id == "btc-bitcoin");
        
        return bitcoin;
    }

    public async Task<CoinPaprikaEntity<List<CoinInfo>>> GetAllCoinsAsync()
    {
        return await _client.GetCoinsAsync();
    }
}