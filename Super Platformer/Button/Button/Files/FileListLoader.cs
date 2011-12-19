using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Button
{
    //<summary>
    // FileListLoader objects take a filepath to a directory.
    // Upon calling the Load function which takes no paramters and return void, 
    // they load all values from the directory.
    // These files paths can then be grabbed from the getter, ListOfFilePaths.
    //</summary>
    public class FileListLoader
    {
        private string mFilePathToDirectory = null;

        public string FilePathToDirectory
        {
            get {
                if (mFilePathToDirectory == null)
                {
                    throw new Exception(this.ToString() + "Null filepath\n\r");
                }

                return mFilePathToDirectory; }
        }
        private List<string> mListOfFilePaths = new List<string>();

        public List<string> ListOfFilePaths
        {
            get { return mListOfFilePaths; }
        }

        FileListLoader(string aFilePathToDirectory)
        {
            mFilePathToDirectory = aFilePathToDirectory;
        }

        void Load()
        {

        }


        public override string ToString()
        {
            return "FileListLoader.cs";
        }
    }
}
