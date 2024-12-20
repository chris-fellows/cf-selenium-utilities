using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver download source
    /// </summary>
    public interface IWebDriverDownloadSource
    {
        
        string BrowserId { get; }

        Task<List<WebDriverSource>> GetListAsync();
    }
}
