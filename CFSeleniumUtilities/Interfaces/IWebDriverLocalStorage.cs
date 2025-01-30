using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver local storage
    /// </summary>
    public interface IWebDriverLocalStorage
    {
        /// <summary>
        /// Adds web driver from source file
        /// </summary>
        /// <param name="webDriverInfo"></param>
        void Add(WebDriverInfo webDriverInfo, string sourceFile);

        /// <summary>
        /// Deletes web driver
        /// </summary>
        /// <param name="webDriverInfo"></param>
        void Delete(WebDriverInfo webDriverInfo);

        bool IsExists(WebDriverInfo webDriverInfo);

        /// <summary>
        /// Gets list
        /// </summary>
        /// <returns></returns>
        List<WebDriverInfo> GetList();
    }
}
