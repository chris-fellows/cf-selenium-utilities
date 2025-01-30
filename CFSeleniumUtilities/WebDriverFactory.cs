using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using OpenQA.Selenium;

namespace CFSeleniumUtilities
{
    public class WebDriverFactory
    {
        private readonly List<IWebDriverFactory> _webDriverFactories;

        public WebDriverFactory(List<IWebDriverFactory> webDriverFactories)
        {
            _webDriverFactories = webDriverFactories;
        }

        /// <summary>
        /// Creates a WebDrive instance from the specified config
        /// </summary>
        /// <param name="webDriverConfig"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IWebDriver Create(WebDriverConfig webDriverConfig)
        {            
            var webDriverFactory = _webDriverFactories.First(wdf => wdf.BrowserId == webDriverConfig.BrowserId);
            return webDriverFactory.Create(webDriverConfig);            
        }
    }
}
