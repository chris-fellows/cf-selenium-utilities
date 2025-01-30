using CFSeleniumUtilities.Enums;
using CFSeleniumUtilities.Interfaces;
using CFSeleniumUtilities.Models;
using CFSeleniumUtilities.Utilities;
using System.ComponentModel.DataAnnotations;
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

            // Copy unzipped files to web driver folder. Try and put the exe in this folder rather than a sub-folder            
            if (!Directory.GetFiles(tempFolder, "*.*").Any() &&
                Directory.GetDirectories(tempFolder).Length == 1)  // Unzipped to single sub-folder, just copy contents of this
            {
                IOUtilities.CopyFolder(Directory.GetDirectories(tempFolder)[0], webDriverFolder);
            }
            else    
            {
                IOUtilities.CopyFolder(tempFolder, webDriverFolder);
            }
                        
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

        public bool IsExists(WebDriverInfo webDriverInfo)
        {
            var webDriverFolder = GetWebDriverFolder(webDriverInfo.BrowserId, webDriverInfo.Version, webDriverInfo.Platform);
            var webDriverInfoFile = Path.Combine(webDriverFolder, "WebDriverInfo.json");

            return File.Exists(webDriverInfoFile);
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
                var webDriverInfo = JSONUtilities.DeserializeFromString<WebDriverInfo>(File.ReadAllText(webDriverInfoFile, Encoding.UTF8), JSONUtilities.DefaultJsonSerializerOptions);

                // Set path to web driver .exe
                var webDrivers = Directory.GetFiles(folder, "*driver.exe");
                if (webDrivers.Any())
                {
                    webDriverInfo.Path = webDrivers.First();
                }
                else    // Check sub-folders for web driver .exe
                {
                    foreach(var subFolder in Directory.GetDirectories(folder))
                    {
                        webDrivers = Directory.GetFiles(subFolder, "*.exe");
                        if (webDrivers.Any())
                        {
                            webDriverInfo.Path = webDrivers.First();
                            break;
                        }
                    }
                }

                return webDriverInfo;
            }

            return null;
        }
    }
}
