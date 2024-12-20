using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using CFSeleniumUtilities.Utilities;
using System.IO.Compression;
using System.Text;

namespace CFSeleniumUtilities
{
    /// <summary>
    /// Web driver local storage.
    /// 
    /// Web drivers are stored in folder \[BrowserProduct]\[Version]\[Platform]
    /// </summary>
    public class WebDriverLocalStorage : IWebDriverLocalStorage
    {
        private readonly string _folder;

        public WebDriverLocalStorage(string folder)
        {
            _folder = folder;
        }

        private string GetWebDriverFolder(string browserId, string version, Platforms platform)
        {
            return Path.Combine(_folder, browserId, version, platform.ToString());
        }

        public void Add(WebDriverInfo webDriverInfo, string sourceFile)
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException($"{sourceFile} does not exist");
            }
            if (!sourceFile.EndsWith(".zip"))
            {
                throw new ArgumentException("Only web driver packages in .zip format can be added");
            }

            var webDriverFolder = GetWebDriverFolder(webDriverInfo.BrowserId, webDriverInfo.Version, webDriverInfo.Platform);
            Directory.CreateDirectory(webDriverFolder);

            // Unzip files. Note that extracting chromedriver-win64 extracts to sub-folder chromedriver-win64
            var tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());            
            using (var archive = ZipFile.Open(sourceFile, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(tempFolder);
                archive.Dispose();
            }

            // Copy files
            IOUtilities.CopyFolder(tempFolder, webDriverFolder);

            //// Set path to exe
            //if (String.IsNullOrEmpty(webDriverInfo.Path))
            //{
            //    webDriverInfo.Path = Directory.GetFiles(webDriverFolder, "*.exe").First();
            //}

            // Delete temp folder            
            Directory.Delete(tempFolder, true);            

            // Save WebDriverInfo.json
            var webDriverInfoFile = Path.Combine(webDriverFolder, "WebDriverInfo.json");
            File.WriteAllText(webDriverInfoFile, JSONUtilities.SerializeToString(webDriverInfo, JSONUtilities.DefaultJsonSerializerOptions), Encoding.UTF8);
        }

        public void Delete(WebDriverInfo webDriverInfo)
        {
            var webDriverFolder = GetWebDriverFolder(webDriverInfo.BrowserId, webDriverInfo.Version, webDriverInfo.Platform);
            if (Directory.Exists(webDriverFolder))
            {
                foreach(var file in Directory.GetFiles(webDriverFolder))
                {
                    File.Delete(file);
                }
                Directory.Delete(webDriverFolder, true);
            }
        }
           
        public List<WebDriverInfo> GetList()
        {           
            var webDriverInfos = new List<WebDriverInfo>();            
            Parallel.ForEach(Directory.GetDirectories(_folder), (browserProductFolder) =>
            {
                foreach (var versionFolder in Directory.GetDirectories(browserProductFolder))
                {
                    foreach (var platformFolder in Directory.GetDirectories(versionFolder))
                    {
                        var webDriverInfo = GetWebDriverInfo(platformFolder);
                        if (webDriverInfo != null)
                        {
                            webDriverInfos.Add(webDriverInfo);
                        }
                    }
                }
            });           

            return webDriverInfos;
        }

        private static WebDriverInfo? GetWebDriverInfo(string folder)
        {
            var webDriverInfoFile = Path.Combine(folder, "WebDriverInfo.json");
            if (File.Exists(webDriverInfoFile))
            {
                var webDriverInfo = JSONUtilities.DeserializeFromString<WebDriverInfo>(webDriverInfoFile, JSONUtilities.DefaultJsonSerializerOptions);

                // Set path to web driver .exe                
                webDriverInfo.Path = Directory.GetFiles(folder, "*.exe").First();                

                return webDriverInfo;
            }

            return null;
        }
    }
}
