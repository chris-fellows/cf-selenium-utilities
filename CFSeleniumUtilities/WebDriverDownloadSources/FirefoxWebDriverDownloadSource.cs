using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.WebDriverDownloadSources
{
    /// <summary>
    /// Download source for Firefox web driver
    /// </summary>
    public class FirefoxWebDriverDownloadSource : IWebDriverDownloadSource
    {
        private string _listURL = "";

        private readonly Browser _browser;

        public FirefoxWebDriverDownloadSource(Browser browser)
        {
            _browser = browser;
        }

        public string BrowserId => _browser.Id;

        public async Task<List<WebDriverSource>> GetListAsync()
        {
            var httpClient = new HttpClient(new HttpClientHandler());

            var response = await httpClient.GetAsync(_listURL);
            var queryResponseContent = response.Content.ReadAsStringAsync().Result;

            var webDriverInfos = new List<WebDriverSource>();

            return webDriverInfos;
        }

        private static Platforms GetPlatform(string platform)
        {
            return Platforms.Win64;
        }
    }
}
