using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SeleniumRestsharp.Core;

public class WebDriverFactory
{
    public IWebDriver CreateDriver(string browserName)
    {
        switch (browserName.ToLower())
        {
            case "chrome":
                var options = new ChromeOptions();
                options.AddArgument("--disable-application-cache");
                options.AddArgument("--disk-cache-size=0");
                options.AddArgument("--incognito");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                if (Config.Headless)
                {
                    options.AddArgument("--headless");
                }
                return new ChromeDriver(options);
            case "firefox":
                return new FirefoxDriver();
            case "edge":
                return new EdgeDriver();
            default:
                throw new ArgumentException($"Browser {browserName} is not supported.");
        }
    }
}