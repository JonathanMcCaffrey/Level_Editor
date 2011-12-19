﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        #region Data
        private string mFilePathToDirectory = null;
        public string FilePathToDirectory
        {
            get
            {
                if (mFilePathToDirectory == null)
                {
                    throw new Exception(this.ToString() + "Null directory filepath\n\r");
                }

                return mFilePathToDirectory;
            }
        }
        private List<string> mListOfFilePaths = new List<string>();
        public List<string> ListOfFilePaths
        {
            get { return mListOfFilePaths; }
        }
        #endregion

        #region Construction
        public FileListLoader(string aFilePathToDirectory)
        {
            mFilePathToDirectory = aFilePathToDirectory;
        }
        #endregion

        #region Methods
        public virtual void Load()
        {
            string[] rawFilePaths = Directory.GetFiles(@mFilePathToDirectory);

            for (int loop = 0; loop < rawFilePaths.Length; loop++)
            {
                mListOfFilePaths.Add(rawFilePaths[loop]);
            }
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "FileListLoader.cs";
        }
        #endregion
        #endregion
    }
}