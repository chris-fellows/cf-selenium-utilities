using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Services
{
    public class XMLBrowserService : XMLEntityWithIdService<Browser, string>, IBrowserService
    {
        public XMLBrowserService(string folder) : base(folder,
                                                    "Browser.*.xml",
                                                    (brower) => $"Browser.{brower.Id}.xml",
                                                    (browerId) => $"Browser.{browerId}.xml")
        {

        }
    }
}
