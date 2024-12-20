using CFSeleniumUtilities.Enums;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web driver configuration
    /// </summary>
    public class WebDriverConfig
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Browser Id
        /// </summary>
        public string BrowserId { get; set; } = String.Empty;

        /// <summary>
        /// Config name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Parameters
        /// </summary>
        public List<WebDriverConfigParameter> Parameters = new List<WebDriverConfigParameter>();
    }
}
