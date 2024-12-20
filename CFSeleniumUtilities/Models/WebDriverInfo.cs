using CFSeleniumUtilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Models
{
    public class WebDriverInfo
    {
        /// <summary>
        /// Browser Id
        /// </summary>
        public string BrowserId { get; set; } = String.Empty;

        /// <summary>
        /// Platform
        /// </summary>
        public Platforms Platform { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; } = String.Empty;

        /// <summary>
        /// Web driver file
        /// </summary>
        public string Path { get; set; } = String.Empty;
    }
}
