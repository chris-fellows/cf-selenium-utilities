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
        private readonly string _webDriverDownloadFolder;      // Folder where web driver packages downloaded to
        private readonly string _webDriverLocalFolder;        // Folder where web driver packages are unzipped to and used from

        public WebDriverDownloadTest(string webDriverDownloadFolder, string webDriverLocalFolder)
        {
            _webDriverDownloadFolder = webDriverDownloadFolder;
            _webDriverLocalFolder = webDriverLocalFolder;
        }   

        /// <summary>
        /// Downloads web driver
        /// </summary>
        /// <param name="browserProduct"></param>
        /// <exception cref="NotSupportedException"></exception>
        public void Download(BrowserProducts browserProduct)
        {
            // Set web driver local storage
            IWebDriverLocalStorage webDriverLocalStorage = new WebDriverLocalStorage(_webDriverLocalFolder);

            switch (browserProduct)
            {
                case BrowserProducts.Chrome:
                    DownloadWebDriver(BrowserProducts.Chrome, new ChromeWebDriverDownloadSource(), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Edge:
                    DownloadWebDriver(BrowserProducts.Edge, new EdgeWebDriverDownloadSource(), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Firefox:
                    DownloadWebDriver(BrowserProducts.Firefox, new FirefoxWebDriverDownloadSource(), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                case BrowserProducts.Opera:
                    DownloadWebDriver(BrowserProducts.Opera, new OperaWebDriverDownloadSource(), 
                                    new HTTPWebDriverDownloader(), webDriverLocalStorage);
                    break;
                default:
                    throw new NotSupportedException($"Download of Web Driver for {browserProduct} is not supportd");
            }
        }

        /// <summary>
        /// Downloads web driver
        /// </summary>
        /// <param name="browserProduct"></param>
        /// <param name="webDriverDownloadSource"></param>
        private void DownloadWebDriver(BrowserProducts browserProduct, 
                                        IWebDriverDownloadSource webDriverDownloadSource,
                                        IWebDriverDownloader webDriverDownloader, 
                                        IWebDriverLocalStorage webDriverLocalStorage)
        {            
            Directory.CreateDirectory(_webDriverDownloadFolder);
            Directory.CreateDirectory(_webDriverLocalFolder);

            // Get web driver download list            
            var webDriverSources = webDriverDownloadSource.GetListAsync().Result;

            // Set web driver to download
            var webDriverSource = webDriverSources.Last();

            // Download web driver package
            var downloadFolder = Path.Combine(_webDriverDownloadFolder, browserProduct.ToString(), webDriverSource.Version, webDriverSource.Platform.ToString());
            webDriverDownloader.DownloadAsync(webDriverSource, downloadFolder).Wait();

            // Get web driver package source
            var webDriverSourceFile = Directory.GetFiles(downloadFolder, "*.zip").First();

            // Unzip web driver package to local storage where it can be used
            var webDriverInfo = new WebDriverInfo()
            {
                BrowserProduct = webDriverSource.BrowserProduct,
                Platform = webDriverSource.Platform,
                Version = webDriverSource.Version
            };
            webDriverLocalStorage.Add(webDriverInfo, webDriverSourceFile);

            // Get all local web drivers
            var webDriverInfos = webDriverLocalStorage.GetList();
        }
    }
}
