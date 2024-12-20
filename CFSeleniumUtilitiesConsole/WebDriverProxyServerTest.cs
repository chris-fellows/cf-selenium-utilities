using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.WebDriverProxyServers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilitiesConsole
{
    /// <summary>
    /// Test web driver proxy server
    /// </summary>
    internal class WebDriverProxyServerTest
    {
        public void Test()
        {
            IWebDriverProxyServer webDriverProxyServer = new LocalProcessWebDriverProxyServer("", "");
            webDriverProxyServer.Start();

            // TODO: Test web driver

            webDriverProxyServer.Stop();
        }
    }
}
