using CFSeleniumUtilities.Enums;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web driver config parameter
    /// </summary>
    public class WebDriverConfigParameter
    {
        /// <summary>
        /// Parameter type
        /// </summary>
        public WebDriverConfigParameterTypes ParameterType { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        public object? Value { get; set; }
    }
}
