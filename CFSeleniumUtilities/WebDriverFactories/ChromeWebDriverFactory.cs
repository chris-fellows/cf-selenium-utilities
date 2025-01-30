using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace CFSeleniumUtilities.WebDriverFactories
{
    /// <summary>
    /// Factory for Chrome WebDriver instances
    /// </summary>
    public class ChromeWebDriverFactory : IWebDriverFactory
    {
        private readonly Browser _browser;
        public ChromeWebDriverFactory(Browser browser)
        {
            _browser = browser;
        }

        public string BrowserId => _browser.Id;

        public IWebDriver Create(WebDriverConfig webDriverConfig)
        {
            var webDriver = new ChromeDriver("D:\\Data\\Test\\WebDriverUtilities\\WebDrivers\\chrome\\132.0.6783.0\\Win64");

            return webDriver;
        }
    }
}
