using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Models
{
    public class VersionInfo
    {
        private string _version = String.Empty;

        public VersionInfo(string version)
        {
            _version = version;
        }

        /// <summary>
        /// Returns array of version elements
        /// </summary>
        public int[] Elements
        {
            get { return _version.Split('.').Select(e => Convert.ToInt32(e)).ToArray(); }
        }            
    }
}
