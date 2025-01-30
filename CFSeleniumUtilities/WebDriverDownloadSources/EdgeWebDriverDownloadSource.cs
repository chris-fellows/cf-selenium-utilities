using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using OpenQA.Selenium;

namespace CFSeleniumUtilities.WebDriverDownloadSources
{
    /// <summary>
    /// Download source for Edge web driver.
    /// 
    /// Uses Selenium to get all download links
    /// </summary>
    public class EdgeWebDriverDownloadSource : IWebDriverDownloadSource
    {        
        private string _listURL = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver?form=MA13LH";

        private readonly Browser _browser;        
        private readonly WebDriverFactory _webDriverFactory;

        public EdgeWebDriverDownloadSource(Browser browser, WebDriverFactory webDriverFactory)
        {
            _browser = browser;
            _webDriverFactory = webDriverFactory;
        }

        public string BrowserId => _browser.Id;

        public async Task<List<WebDriverSource>> GetListAsync()
        {
            var webDriverSources = new List<WebDriverSource>();

            using (var webDriver = _webDriverFactory.Create(new WebDriverConfig() { BrowserId = "chrome" }))
            {
                webDriver.Url = _listURL;
                webDriver.Navigate();

                var elements = webDriver.FindElements(By.TagName("a"));
                foreach (var anchor in elements)
                {
                    var href = anchor.GetAttribute("href");
                    if (!String.IsNullOrEmpty(href) &&
                        href.ToLower().Contains("edgedriver"))
                    {
                        var urlElements = href.Split('/');

                        // https://msedgedriver.azureedge.net/132.0.2957.127/edgedriver_linux64.zip

                        var platform = GetPlatformFromFile(urlElements.Last());
                        if (platform != null)
                        {
                            var webDriverSource = new WebDriverSource(_browser.Id,
                                                (Platforms)platform,
                                                href,
                                                urlElements[urlElements.Length - 2]);
                            webDriverSources.Add(webDriverSource);
                        }
                    }
                }

                webDriver.Close();
            }
            
            return webDriverSources.OrderBy(i => i.Version).ThenBy(i => i.BrowserId).ToList();
        }

        private static Platforms? GetPlatformFromFile(string file)
        {
            return file switch
            {
                    //"edgedriver_arm64.zip" => Platforms.A,
                    "edgedriver_linux64.zip" => Platforms.Linux64,
                    "edgedriver_mac64.zip" => Platforms.MacX32,
                    "edgedriver_mac32.zip" => Platforms.MacX64,
                    "edgedriver_win32.zip" => Platforms.Win32,
                    "edgedriver_win64.zip" => Platforms.Win64,
                    _ => null
            };            
        }
    }
}
