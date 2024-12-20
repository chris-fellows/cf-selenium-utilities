using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.SeedData
{
    /// <summary>
    /// Seed for WebDriverConfig instances
    /// </summary>
    public class WebDriverConfigSeed
    {
        public List<WebDriverConfig> GetAll(List<Browser> browsers)
        {
            var webDriverConfigs = new List<WebDriverConfig>();

            foreach(var browser in browsers)
            {
                // Add default
                var webDriverConfig1 = new WebDriverConfig()
                {
                    Id = Guid.NewGuid().ToString(),
                    BrowserId = browser.Id,
                    Name = $"{browser.Name} (Default)",
                    Parameters = new List<WebDriverConfigParameter>()
                };
                webDriverConfigs.Add(webDriverConfig1);

                //// Add BrowserStack
                //var webDriverConfig2 = new WebDriverConfig()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    BrowserId = browser.Id,
                //    Name = $"{browser.Name} (BrowserStack)",
                //    Parameters = new List<WebDriverConfigParameter>()
                //    {
                //        new WebDriverConfigParameter()
                //        {
                //            ParameterType = WebDriverConfigParameterTypes.WebDriverProxyServerFilePath,
                //            Value = "D:\\Test\\WebDriverProxyServer\\BrowserStack\\BrowerStack.exe",
                //        },
                //        new WebDriverConfigParameter()
                //        {
                //            ParameterType = WebDriverConfigParameterTypes.WebDriverProxyServerFileArguments,
                //            Value = ""
                //        }
                //    }
                //};
                //webDriverConfigs.Add(webDriverConfig1);
            }

            return webDriverConfigs;
        }
    }
}
