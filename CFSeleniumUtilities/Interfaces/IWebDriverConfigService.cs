using CFSeleniumUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Interfaces
{
    /// <summary>
    /// Web driver config service for managing WebDriverConfig instances
    /// </summary>
    public interface IWebDriverConfigService : IEntityWithIdService<WebDriverConfig, string>
    {
        
    }
}
