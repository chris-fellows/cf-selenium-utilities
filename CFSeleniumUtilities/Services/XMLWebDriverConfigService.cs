using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Services
{
    public class XMLWebDriverConfigService : XMLEntityWithIdService<WebDriverConfig, string>, IWebDriverConfigService
    {
        public XMLWebDriverConfigService(string folder) : base(folder,
                                                  "WebDriverConfig.*.xml",
                                                  (brower) => $"WebDriverConfig.{brower.Id}.xml",
                                                  (browerId) => $"WebDriverConfig.{browerId}.xml")
        {

        }
    }
}
