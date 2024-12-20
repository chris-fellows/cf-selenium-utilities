using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web driver proxy server config. E.g. BrowserStack proxy server config
    /// </summary>
    public class WebDriverProxyServerConfig
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string ProxyServerFilePath { get; set; } = String.Empty;

        public string ProxyServerFileArguments { get; set; } = String.Empty;
    }
}
