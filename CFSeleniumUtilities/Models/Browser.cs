using CFSeleniumUtilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Models
{
    /// <summary>
    /// Web browser
    /// </summary>
    public class Browser
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Browser name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Browser product
        /// </summary>
        public BrowserProducts BrowserProduct { get; set; }
    }
}
