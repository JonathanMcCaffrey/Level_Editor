using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelEditor
{
    //<summary>
    // Inherits from FileListLoader.
    // Contains specific schematics for loading files for the editor.
    //</summary>
    public class EditorAssetLoader : FileListLoader
    {
        #region Data
        private List<string> mSortedModelFiles = new List<string>();
        public List<string> SortedModelFiles
        {
            get
            {
                if (mSortedModelFiles == null)
                {
                    Console.WriteLine("{0} did not load/is empty. {1}.", "mSortedModelFiles", this.ToString());
                }

                return mSortedModelFiles;
            }
        }

        private List<string> mStortedIconFiles = new List<string>();
        public List<string> StortedIconFiles
        {
            get
            {
                if (mStortedIconFiles == null)
                {
                    Console.WriteLine("{0} did not load/is empty. {1}.", "mStortedIconFiles", this.ToString());
                }

                if (mSortedModelFiles != null && mSortedModelFiles != null && (mSortedModelFiles.Count != mStortedIconFiles.Count))
                {
                    Console.WriteLine("{0} does not have the same amount of graphics as models. {1}.", "mStortedIconFiles", this.ToString());
                }

                return mStortedIconFiles;
            }
        }

        private List<string> mStortedTextureFiles = new List<string>();
        public List<string> StortedTextureFiles
        {
            get
            {
                if (mStortedTextureFiles == null)
                {
                    Console.WriteLine("{0} did not load/is empty. {1}.", "mStortedTextureFiles", this.ToString());
                }

                if (mSortedModelFiles != null && mSortedModelFiles != null && (mSortedModelFiles.Count != mStortedIconFiles.Count))
                {
                    Console.WriteLine("{0} does not have the same amount of graphics as models. {1}.", "mStortedTextureFiles", this.ToString());
                }


                return mStortedTextureFiles;
            }
        }
        #endregion

        #region Construction
        public EditorAssetLoader(string aFilePathToDirectory)
            : base(aFilePathToDirectory)
        {

        }
        #endregion

        #region Methods
        public override void Load()
        {
            base.Load();

            for (int loop = 0; loop < ListOfFilePaths.Count; loop++)
            {
                string[] tempOrganizedData = new string[2];

                string rawData = ListOfFilePaths[loop];

                tempOrganizedData = rawData.Split('.'); // TODO: There may be a bug here with the '.' Add test case

                switch (tempOrganizedData[1])
                {
                    case "obj":
                        mSortedModelFiles.Add(ListOfFilePaths[loop]);
                        break;

                    case "jpg":
                        Console.WriteLine(ListOfFilePaths[loop]);
                        mStortedIconFiles.Add(ListOfFilePaths[loop]);
                        break;

                    case "png":
                        Console.WriteLine(ListOfFilePaths[loop]);
                        mStortedTextureFiles.Add(ListOfFilePaths[loop]);
                        break;

                    default:
                        break;
                }
            }
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "EditorAssetLoader.cs";
        }
        #endregion
        #endregion
    }
}
