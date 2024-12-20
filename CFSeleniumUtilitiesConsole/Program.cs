using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.SeedData;
using CFSeleniumUtilities.Services;
using CFSeleniumUtilitiesConsole;

// See https://aka.ms/new-console-template for more information

var dataFolder = "D:\\Data\\Test\\WebDriverUtilities\\Data";
Directory.CreateDirectory(dataFolder);

IBrowserService browserService = new XMLBrowserService(Path.Combine(dataFolder, "Browsers"));
IWebDriverConfigService webDriverConfigService = new XMLWebDriverConfigService(Path.Combine(dataFolder, "WebDriverConfigs"));
IWebDriverProxyServerConfigService webDriverProxyServerConfigService = new XMLWebDriverProxyServerConfigService(Path.Combine(dataFolder, "WebDriverProxyServerConfigs"));

var browsers = browserService.GetAll();

// Initialise data if required
if (!browsers.Any())
{
    // Initialise browsers
    var browsersSeed = new BrowserSeed().GetAll();
    browsersSeed.ForEach(browser => browserService.Add(browser));

    // Initialise web driver configs
    var webDriverConfigsSeed = new WebDriverConfigSeed().GetAll(browsersSeed);
    webDriverConfigsSeed.ForEach(webDriverConfig => webDriverConfigService.Add(webDriverConfig));

    // Initialise web driver proxy server configs
    var webDriverProxyServerConfigsSeed = new WebDriverProxyServerConfigSeed().GetAll();
    webDriverProxyServerConfigsSeed.ForEach(config => webDriverProxyServerConfigService.Add(config));

    // Refresh browser list
    browsers = browserService.GetAll();
}

var browser = browsers.First(b => b.BrowserProduct == BrowserProducts.Chrome);

new WebDriverDownloadTest("D:\\Test\\WebDriverUtilities\\WebDriverDownloads",
                    "D:\\Test\\WebDriverUtilities\\WebDrivers").Download(browser);

int xxx = 1000;



