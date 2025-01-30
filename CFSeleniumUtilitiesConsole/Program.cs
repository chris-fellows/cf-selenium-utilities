using CFSeleniumUtilities;
using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.SeedData;
using CFSeleniumUtilities.Services;
using CFSeleniumUtilities.WebDriverFactories;
using CFSeleniumUtilitiesConsole;
using OpenQA.Selenium;
using System.Net.WebSockets;

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

// Create web driver factory
var webDriverFactories = new List<IWebDriverFactory>();
foreach (var currentBrower in browsers)
{
    switch (currentBrower.BrowserProduct)
    {
        case BrowserProducts.Chrome: webDriverFactories.Add(new ChromeWebDriverFactory(currentBrower)); break;
        case BrowserProducts.Edge: webDriverFactories.Add(new EdgeWebDriverFactory(currentBrower)); break;
        case BrowserProducts.Firefox: webDriverFactories.Add(new FirefoxWebDriverFactory(currentBrower)); break;
        case BrowserProducts.Safari: webDriverFactories.Add(new SafariWebDriverFactory(currentBrower)); break;
        case BrowserProducts.Opera: webDriverFactories.Add(new OperaWebDriverFactory(currentBrower)); break;
    }
}
var webDriverFactory = new WebDriverFactory(webDriverFactories);

//var browser = browsers.First(b => b.BrowserProduct == BrowserProducts.Chrome);
var browser = browsers.First(b => b.BrowserProduct == BrowserProducts.Edge);

new WebDriverDownloadTest(webDriverFactory,
                    "D:\\Data\\Test\\WebDriverUtilities\\WebDriverDownloads",
                    "D:\\Data\\Test\\WebDriverUtilities\\WebDrivers").Download(browser);

int xxx = 1000;



