namespace Playwright.Core.Helpers;

public static class TestHelper
{
    private static readonly Random _random = new();
    
    public static T GetRandomElement<T>(IEnumerable<T> items)
    {
        var list = items.ToList();
        return list.Count > 0 ? list[_random.Next(list.Count)] : default;
    }
    
    public static string GenerateRandomString(int length = 10)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}