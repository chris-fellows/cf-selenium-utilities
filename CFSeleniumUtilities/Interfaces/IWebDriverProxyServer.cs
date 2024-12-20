namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver proxy server. E.g. BrowserStack
    /// </summary>
    public interface IWebDriverProxyServer
    {
        /// <summary>
        /// Whether proxy server is active
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Start proxy server
        /// </summary>
        void Start();

        /// <summary>
        /// Stop proxy server
        /// </summary>
        void Stop();
    }
}
