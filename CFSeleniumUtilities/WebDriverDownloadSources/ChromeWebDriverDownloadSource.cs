using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System.Text.Json.Nodes;

namespace CFSeleniumUtilities.WebDriverDownloadSources
{
    /// <summary>
    /// Download source for Chrome web driver
    /// </summary>
    public class ChromeWebDriverDownloadSource : IWebDriverDownloadSource
    {
        private string _listURL = "https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json";

        private readonly Browser _browser;

        public ChromeWebDriverDownloadSource(Browser browser)
        {
            _browser = browser;
        }

        public string BrowserId => _browser.Id;

        public async Task<List<WebDriverSource>> GetListAsync()
        {
            var httpClient = new HttpClient(new HttpClientHandler());

            var response = await httpClient.GetAsync(_listURL);
            var queryResponseContent = response.Content.ReadAsStringAsync().Result;

            //dynamic data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(queryResponseContent);            
            //dynamic item = data[1];

            // Deserialise
            var data = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(queryResponseContent);
            var versions = (JsonArray)data["versions"];

            // Process versions
            var webDriverInfos = new List<WebDriverSource>();
            foreach (var versionItem in versions)
            {
                var downloads = (JsonObject)versionItem["downloads"];
                if (downloads.ContainsKey("chromedriver"))
                {
                    //var chrome = (JsonArray)((JsonObject)versionItem["downloads"])["chromedriver"];
                    var chromeDriver = (JsonArray)downloads["chromedriver"];
                    foreach (var item in chromeDriver)
                    {
                        var platform = GetPlatform((string)item["platform"]);
                        var webDriverInfo = new WebDriverSource(_browser.Id, platform, (string)item["url"], (string)versionItem["version"]);
                        webDriverInfos.Add(webDriverInfo);
                    }
                }
                else
                {
                    int xxx = 1000;
                }
            }           

            return webDriverInfos;
        }

        private static Platforms GetPlatform(string platform)
        {
            return platform switch
            {
                "linux32" => Platforms.Linux32,
                "linux64" => Platforms.Linux64,
                "mac-arm64" => Platforms.MacARM64,
                "mac-x64" => Platforms.MacX64,
                "win32" => Platforms.Win32,
                "win64" => Platforms.Win64,
                _ => Platforms.Win64
            };
        }
    }
}
