using CFSeleniumUtilities.Interfaces;

namespace CFSeleniumUtilities.WebDriverProxyServers
{
    /// <summary>
    /// Web driver proxy running as local process
    /// </summary>
    public class LocalProcessWebDriverProxyServer : IWebDriverProxyServer
    {
        private readonly string _processFile;
        private readonly string _processArguments;

        public LocalProcessWebDriverProxyServer(string processFile, string processArguments)
        {
            _processFile = processFile;
            _processArguments = processArguments;
        }

        public bool IsActive
        {
            get { return false; }
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
