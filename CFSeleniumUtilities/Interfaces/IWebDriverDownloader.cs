using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver downloader
    /// </summary>
    public interface IWebDriverDownloader
    {        
        Task DownloadAsync(WebDriverSource webDriverInfo, string folder);
    }
}
