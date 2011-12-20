using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Button
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
                    throw new Exception(this.ToString() + "\n\rModel files did not load\n\r");
                }

                return mSortedModelFiles;
            }
        }

        private List<string> mStortedTextureFiles = new List<string>();
        public List<string> StortedTextureFiles
        {
            get
            {
                if (mStortedTextureFiles == null)
                {
                    throw new Exception(this.ToString() + "\n\rTexture files did not load\n\r");
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
