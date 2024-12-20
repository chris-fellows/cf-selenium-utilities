using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;

namespace CFSeleniumUtilities.WebDriverDownloaders
{
    /// <summary>
    /// Downloads web drivers via HTTP
    /// </summary>
    public class HTTPWebDriverDownloader : IWebDriverDownloader
    {
        public async Task DownloadAsync(WebDriverSource webDriverSource, string folder)
        {
            Directory.CreateDirectory(folder);

            // Request file
            var httpClient = new HttpClient(new HttpClientHandler());            
            var response = await httpClient.GetAsync(webDriverSource.URL);
                        
            // Set local file
            var fileName = webDriverSource.URL.Split('/').Last();
            var filePath = Path.Combine(folder, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);

            // Read the content into a MemoryStream and then write to file
            using (var memoryStream = await response.Content.ReadAsStreamAsync())
            {
                using (var fileStream = File.Create(filePath))
                {
                    await memoryStream.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
            }
        }
    }
}
