using CFSeleniumUtilities.Enums;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web driver source
    /// </summary>
    public class WebDriverSource
    {
        /// <summary>
        /// Browser Id
        /// </summary>
        public string BrowserId { get; internal set; } = String.Empty;

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

        public WebDriverSource(string browserId, Platforms platform, string url, string version)
        {
            BrowserId = browserId;
            Platform = platform;
            URL = url;
            Version = version;
        }
    }
}
