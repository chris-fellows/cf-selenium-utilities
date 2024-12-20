using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.SeedData
{
    public class WebDriverProxyServerConfigSeed
    {
        public List<WebDriverProxyServerConfig> GetAll()
        {
            var configs = new List<WebDriverProxyServerConfig>();

            var config = new WebDriverProxyServerConfig()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "BrowserStack",
                ProxyServerFilePath = "D:\\Test\\WebDriverUtilities\\WebDriverProxyServers\\BrowserStack\\BrowserStack.exe",
                ProxyServerFileArguments = ""
            };
            configs.Add(config);

            return configs;
        }
    }
}
