using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.WebDriverFactories
{
    /// <summary>
    /// Factory for Opera WebDriver instances
    /// </summary>
    public class OperaWebDriverFactory : IWebDriverFactory
    {
        private readonly Browser _browser;
        public OperaWebDriverFactory(Browser browser)
        {
            _browser = browser;
        }

        public string BrowserId => _browser.Id;

        public object Create(WebDriverConfig webDriverConfig)
        {
            throw new NotImplementedException();
        }
    }
}
