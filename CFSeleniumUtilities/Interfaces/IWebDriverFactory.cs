﻿using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver factory
    /// </summary>
    public interface IWebDriverFactory
    {        
        /// <summary>
        /// Browser Id
        /// </summary>
        string BrowserId { get; }

        /// <summary>
        /// Creates web driver
        /// </summary>
        /// <param name="webDriverConfig"></param>
        /// <returns></returns>
        object Create(WebDriverConfig webDriverConfig);
    }
}