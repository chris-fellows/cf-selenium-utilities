using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.SeedData
{
    /// <summary>
    /// Seed for Browser instances
    /// </summary>
    public class BrowserSeed
    {
        public List<Browser> GetAll()
        {
            var browsers = new List<Browser>()
            {
                new Browser()
                {
                    Id = "chrome",
                    Name = "Chrome",
                    BrowserProduct = BrowserProducts.Chrome
                },
                new Browser()
                {
                    Id = "edge",
                    Name = "Edge",
                    BrowserProduct = BrowserProducts.Edge
                },
                new Browser()
                {
                    Id = "firefox",
                    Name = "Firefox",
                    BrowserProduct = BrowserProducts.Firefox
                },
                new Browser()
                {
                    Id = "opera",
                    Name = "Opera",
                    BrowserProduct = BrowserProducts.Opera
                },
                new Browser()
                {
                    Id = "safari",
                    Name = "Safari",
                    BrowserProduct = BrowserProducts.Safari
                },
            };

            return browsers;
        }
    }
}
