using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Button
{
    public partial class AssetsWindow : Form
    {
        EditorAssetLoader mEditorAssetLoader = new EditorAssetLoader(@"C:\Users\mcca0442\Desktop\trunk\Super Platformer\Button\ButtonContent\Assets");

        private bool mIsHoveringOnEditor = false;

        public AssetsWindow()
        {
            InitializeComponent();


            /* this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
             this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;*/
            this.TopMost = false;

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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
