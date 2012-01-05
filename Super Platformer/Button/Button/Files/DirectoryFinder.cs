using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace LevelEditor
{
    //<summary>
    // Contains helper methods for finding directories.
    //</summary>
    public static class DirectoryFinder
    {
        #region Methods
        public static string FindProjectDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string directoryPath = Path.GetDirectoryName(path);
            directoryPath = directoryPath.Replace("\\Button\\bin\\x86\\Debug", "\\");
            Console.WriteLine(directoryPath);

            return directoryPath;
        }

        public static string FindEditorGameAssetDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string directoryPath = Path.GetDirectoryName(path);
            directoryPath = directoryPath.Replace("\\Button\\bin\\x86\\Debug", "\\ButtonContent\\Assets\\");
            Console.WriteLine(directoryPath);

            return directoryPath;
        }

        public static string FindContentDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string directoryPath = Path.GetDirectoryName(path);
            directoryPath = directoryPath.Replace("\\Button\\bin\\x86\\Debug", "\\ButtonContent\\");
            Console.WriteLine(directoryPath);

            return directoryPath;
        }
        #endregion
    }
}
