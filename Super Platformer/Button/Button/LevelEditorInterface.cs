using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Button
{
    public partial class LevelEditorInterface : Form
    {
        #region Data
        /*   private LevelEditorInterface mLevelEditorInterface = null;
        public LevelEditorInterface LevelEditorInterface
        {
            set
            {
                mLevelEditorInterface = value;
            }
        }*/

        EditorAssetLoader mEditorAssetLoader = new EditorAssetLoader(@"C:\Users\mcca0442\Desktop\trunk\Super Platformer\Button\ButtonContent\Assets");

        private bool mIsHoveringOnEditor = false;
        
        #endregion

        #region Construction
        public LevelEditorInterface()
        {
            /* this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
             this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;*/
            this.TopMost = false;

            InitializeComponent();
            InitializeImages();

            mEditorAssetLoader.Load();

            List<string> tempList = mEditorAssetLoader.StortedIconFiles;

            ImageList tempImageList = new ImageList();

            for (int loop = 0; loop < tempList.Count; loop++)
            {
                tempImageList.Images.Add(Image.FromFile(tempList[loop]));
                iAssetList.Items.Add(tempList[loop]);
                iAssetList.Items[loop].BackColor = System.Drawing.Color.White;
                iAssetList.Items[loop].Name = tempList[loop];
                iAssetList.LargeImageList = tempImageList;
                iAssetList.Items[loop].ImageIndex = loop;
            }
        }

        /** Windows Form cannot preload images with XNA 4.0. Microsoft stated the problem will not be fixed in future updates.
            So, this function call intializes all images in the interface. */
        private void InitializeImages()
        {
            itNew.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\New.jpg");
            itOpen.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Open.jpg");
            itSave.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Save.jpg");
            itUndo.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Undo.jpg");
            itRedo.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Redo.jpg");
            itArrow.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Arrow.jpg");
            itTranslate.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Translate.jpg");
            itRotate.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Rotate.jpg");
            itScale.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Scale.jpg");
            itScaleLinear.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\ScaleLinear.jpg");

            itAdd.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Add.jpg");
            itSubtract.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Subtract.jpg");
            itFlatten.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Flatten.jpg");
            itSmooth.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Smooth.jpg");
            itNoise.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Noise.jpg");

            // Placeholder
            iTopGraphic.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Noise.jpg");
            iFrontGraphic.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Flatten.jpg");
            iRightGraphic.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Subtract.jpg");
            // Placeholder
        }
        #endregion

        #region Methods
        public void UpdateWindow()
        {
            RenderTarget2D tempTextureToConvert = FileManager.Get().EditorWorkAreaRenderTexture2D;

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);
            
            tempMemoryStream.Close();
            tempMemoryStream.Dispose();
            tempMemoryStream = null;

            iPerspectiveGraphic.Image = tempImageToUpdate;

            Invalidate();

            if (mIsHoveringOnEditor)
            {
                InputManager.Get().MousePositionOnWindow = new Vector2(MousePosition.X - this.Location.X - 16, MousePosition.Y - this.Location.Y -273);
            }
        }

        private void itOpen_Click(object sender, EventArgs aEvent)
        {
            OpenFileDialog tempFileDialog = new OpenFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        private void itSave_Click(object sender, EventArgs aEvent)
        {
            SaveFileDialog tempFileDialog = new SaveFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        void iGameGraphic_MouseDoubleClick(object sender, MouseEventArgs aMouseEvent)
        {
            Vector2 tempMousePosition = new Vector2(aMouseEvent.X, aMouseEvent.Y);

            ButtonManager.Get().GenerateEntity(new Vector3(tempMousePosition.X, 0, tempMousePosition.Y));
        }

        void iGameGraphic_MouseClick(object sender, MouseEventArgs aMouseEvent)
        {
            Vector2 tempMousePosition = new Vector2(aMouseEvent.X, aMouseEvent.Y);
        }
        #endregion

        private void iPerspectiveGraphic_Click(object sender, MouseEventArgs e)
        {
            InputManager.Get().MousePositionOnWindow = new Vector2(e.X, e.Y);
        }

        void iPerspectiveGraphic_MouseHover(object sender, EventArgs e)
        {
            mIsHoveringOnEditor = true;
        }

        void iPerspectiveGraphic_MouseLeave(object sender, EventArgs e)
        {
            mIsHoveringOnEditor = false;
        }

        private void itNew_Click(object sender, EventArgs e)
        {
            EntityComponetManager.Get().Clear();
        }

        private void iAssetList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}