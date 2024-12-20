using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Services
{
    public class XMLWebDriverProxyServerConfigService : XMLEntityWithIdService<WebDriverProxyServerConfig, string>, IWebDriverProxyServerConfigService
    {
        public XMLWebDriverProxyServerConfigService(string folder) : base(folder,
                                              "WebDriverProxyServerConfig.*.xml",
                                              (brower) => $"WebDriverProxyServerConfig.{brower.Id}.xml",
                                              (browerId) => $"WebDriverProxyServerConfig.{browerId}.xml")
        {

        }
    }
}
