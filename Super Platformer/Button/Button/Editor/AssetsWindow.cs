using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelEditor
{
    public partial class AssetsWindow : Form
    {
        #region Fields
        EditorAssetLoader mEditorAssetLoader = new EditorAssetLoader();
        Tile mSelectedTile;
        private bool mIsHoveringOnEditor = false;
        #endregion

        #region Construction
        public AssetsWindow()
        {
            InitializeComponent();

            mSelectedTile = new Tile();
            GameFiles.CurrentTile = mSelectedTile;

            mEditorAssetLoader.Load();

            List<string> tempList = mEditorAssetLoader.StortedIconFiles;

            ImageList tempImageList = new ImageList();

            for (int loop = 0; loop < tempList.Count; loop++)
            {
                tempImageList.Images.Add(Image.FromFile(tempList[loop]));
                tempImageList.ImageSize = new Size(128, 128);
                iAssetList.Items.Add(tempList[loop]);
                iAssetList.Items[loop].BackColor = System.Drawing.Color.White;
                iAssetList.Items[loop].Name = tempList[loop];
                iAssetList.LargeImageList = tempImageList;
                iAssetList.Items[loop].ImageIndex = loop;
            }
        }
        #endregion

        #region Methods
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tempName = iAssetList.SelectedItems[0].Name;


            tempName = tempName.Replace("jpg", "obj");   // Not working

            mSelectedTile.FilePathToModel = tempName;
            mSelectedTile.FilePathToGraphic = iAssetList.SelectedItems[0].Name;

            string otherTempName = iAssetList.SelectedItems[0].Name;
            string[] sorted = otherTempName.Split('\\');
            otherTempName = sorted[sorted.Length - 1];
            otherTempName = otherTempName.Replace(".jpg", "");

            mSelectedTile.FilePathToGraphic = otherTempName;
        }
        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}