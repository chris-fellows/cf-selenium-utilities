using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using CFSeleniumUtilities.WebDriverDownloaders;
using CFSeleniumUtilities.WebDriverDownloadSources;
using CFSeleniumUtilities;

namespace CFSeleniumUtilitiesConsole
{
    /// <summary>
    /// Tests download of web driver   
    /// </summary>
    internal class WebDriverDownloadTest
    {
        private readonly WebDriverFactory _webDriverFactory;
        private readonly string _webDriverDownloadFolder;      // Folder where web driver packages downloaded to
        private readonly string _webDriverLocalFolder;        // Folder where web driver packages are unzipped to and used from

        public WebDriverDownloadTest(WebDriverFactory webDriverFactory,
                                string webDriverDownloadFolder, string webDriverLocalFolder)
        {
            _webDriverFactory = webDriverFactory;
            _webDriverDownloadFolder = webDriverDownloadFolder;
            _webDriverLocalFolder = webDriverLocalFolder;
        }   

        /// <summary>
        /// Downloads web driver
        /// </summary>
        /// <param name="browserProduct"></param>
        /// <exception cref="NotSupportedException"></exception>
        public void Download(Browser browser)
        {
            // Set web driver local storage
            IWebDriverLocalStorage webDriverLocalStorage = new WebDriverLocalStorage(_webDriverLocalFolder);

            switch (browser.BrowserProduct)
            {
                case BrowserProducts.Chrome:
                    DownloadWebDriver(browser, new ChromeWebDriverDownloadSource(browser), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Edge:
                    DownloadWebDriver(browser, new EdgeWebDriverDownloadSource(browser, _webDriverFactory), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Firefox:
                    DownloadWebDriver(browser, new FirefoxWebDriverDownloadSource(browser), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Opera:
                    DownloadWebDriver(browser, new OperaWebDriverDownloadSource(browser), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                default:
                    throw new NotSupportedException($"Download of Web Driver for {browser.BrowserProduct} is not supportd");
            }
        }

        /// <summary>
        /// Downloads web driver
        /// </summary>
        /// <param name="browserProduct"></param>
        /// <param name="webDriverDownloadSource"></param>
        private void DownloadWebDriver(Browser browser,
                                        IWebDriverDownloadSource webDriverDownloadSource,
                                        IWebDriverDownloader webDriverDownloader, 
                                        IWebDriverLocalStorage webDriverLocalStorage)
        {            
            Directory.CreateDirectory(_webDriverDownloadFolder);
            Directory.CreateDirectory(_webDriverLocalFolder);

            // Get web driver download list            
            var webDriverSources = webDriverDownloadSource.GetListAsync().Result;

            foreach (var webDriverSource in webDriverSources.Where(s => s.Version.StartsWith("132.")))
            {
                var webDriverInfo = new WebDriverInfo()
                {
                    BrowserId = browser.Id,
                    Platform = webDriverSource.Platform,
                    Version = webDriverSource.Version
                };

                if (!webDriverLocalStorage.IsExists(webDriverInfo)) // Not downloaded already
                {
                    // Set web driver to download
                    //var webDriverSource = webDriverSources.Last();

                    // Download web driver package
                    var downloadFolder = Path.Combine(_webDriverDownloadFolder, browser.Id, webDriverSource.Version, webDriverSource.Platform.ToString());
                    webDriverDownloader.DownloadAsync(webDriverSource, downloadFolder).Wait();

                    // Get web driver package source
                    var webDriverSourceFile = Directory.GetFiles(downloadFolder, "*.zip").First();

                    // Unzip web driver package to local storage where it can be used                    
                    webDriverLocalStorage.Add(webDriverInfo, webDriverSourceFile);
                }
            }

            // Get all local web drivers
            var webDriverInfos = webDriverLocalStorage.GetList();
        }
    }
}
