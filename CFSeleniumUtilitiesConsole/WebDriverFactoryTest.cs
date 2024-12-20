using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.WebDriverFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilitiesConsole
{
    /// <summary>
    /// Web driver factory test
    /// </summary>
    public class WebDriverFactoryTest
    {
        public void Create(string webDriverConfigName)
        {
            IWebDriverConfigService webDriverConfigService;

            // Get all web driver configs
            var webDriverConfig = webDriverConfigService.GetAllAsync()
                        .Result.FirstOrDefault(c => c.Name == webDriverConfigName);

            if (webDriverConfig == null)
            {
                throw new ArgumentException($"Web driver config {webDriverConfigName} does not exist");
            }

            // Get web driver factory
            var webDriverFactory = GetWebDriverFactory(webDriverConfig.BrowserProduct);

            // Create web driver
            var webDriver = webDriverFactory.Create(webDriverConfig);
                       
        }

        private IWebDriverFactory GetWebDriverFactory(BrowserProducts browserProduct)
        {
            return browserProduct switch
            {
                BrowserProducts.Chrome => new ChromeWebDriverFactory(),
                BrowserProducts.Edge => new EdgeWebDriverFactory(),
                BrowserProducts.Firefox => new FirefoxWebDriverFactory(),
                BrowserProducts.Opera => new OperaWebDriverFactory(),
                BrowserProducts.Safari => new SafariWebDriverFactory(),
            };
        }
    }
}
