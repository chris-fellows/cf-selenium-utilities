using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Utilities
{
    public static class IOUtilities
    {
        /// <summary>
        /// Copies source folder to destination
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destinationFolder"></param>
        public static void CopyFolder(string sourceFolder, string destinationFolder)
        {
            Directory.CreateDirectory(destinationFolder);

            // Copy files
            foreach(var sourceFile in Directory.GetFiles(sourceFolder))
            {
                var destinationFile = Path.Combine(destinationFolder, Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile);
            }

            // Copy sub-folders
            foreach(var sourceSubFolder in Directory.GetDirectories(sourceFolder))
            {
                //var destinationSubFolder = Path.Combine(destinationFolder, new DirectoryInfo(Path.GetDirectoryName(sourceSubFolder)).Name);
                var destinationSubFolder = Path.Combine(destinationFolder, new DirectoryInfo(sourceSubFolder).Name);
                CopyFolder(sourceSubFolder, destinationSubFolder);
            }
        }
    }
}
