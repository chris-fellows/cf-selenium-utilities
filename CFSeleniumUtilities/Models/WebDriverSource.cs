using CFSeleniumUtilities.Enums;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web driver source
    /// </summary>
    public class WebDriverSource
    {
        /// <summary>
        /// Target web browser
        /// </summary>
        public BrowserProducts BrowserProduct { get; internal set; }

        /// <summary>
        /// Target platform (Linux64, Win64 etc)
        /// </summary>
        public Platforms Platform { get; internal set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Download URL
        /// </summary>
        public string URL { get; internal set; }

        public WebDriverSource(BrowserProducts browserProduct, Platforms platform, string url, string version)
        {
            BrowserProduct = browserProduct;
            Platform = platform;
            URL = url;
            Version = version;
        }
    }
}
