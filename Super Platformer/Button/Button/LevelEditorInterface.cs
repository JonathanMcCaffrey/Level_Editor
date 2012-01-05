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

namespace LevelEditor
{
    public partial class LevelEditorInterface : Form
    {
        #region Fields
        private bool mIsHoveringOnEditor = false;
        #endregion

        #region Properties

        public TabControl Views
        {
            get { return iViews; }
        }

        #endregion

        #region Construction
        public LevelEditorInterface()
        {
            /* this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
             this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;*/
            this.TopMost = false;

            InitializeComponent();
            InitializeImages();
        }

        /** Windows Form cannot preload images with XNA 4.0. Microsoft stated the problem will not be fixed in future updates.
            So, this function call intializes all images in the interface. */
        private void InitializeImages()
        {
            string tempFilePathToAssetDirectory = DirectoryFinder.FindContentDirectory(); 

            itNew.Image = Image.FromFile(tempFilePathToAssetDirectory + "New.jpg");
            itOpen.Image = Image.FromFile(tempFilePathToAssetDirectory + "Open.jpg");
            itSave.Image = Image.FromFile(tempFilePathToAssetDirectory + "Save.jpg");
            itUndo.Image = Image.FromFile(tempFilePathToAssetDirectory + "Undo.jpg");
            itRedo.Image = Image.FromFile(tempFilePathToAssetDirectory + "Redo.jpg");
            itArrow.Image = Image.FromFile(tempFilePathToAssetDirectory + "Arrow.jpg");
            itTranslate.Image = Image.FromFile(tempFilePathToAssetDirectory + "Translate.jpg");
            itRotate.Image = Image.FromFile(tempFilePathToAssetDirectory + "Rotate.jpg");
            itScale.Image = Image.FromFile(tempFilePathToAssetDirectory + "Scale.jpg");
            itScaleLinear.Image = Image.FromFile(tempFilePathToAssetDirectory + "ScaleLinear.jpg");

            itAdd.BackgroundImage = Image.FromFile(tempFilePathToAssetDirectory + "Add.jpg");
            itSubtract.BackgroundImage = Image.FromFile(tempFilePathToAssetDirectory + "Subtract.jpg");
            itFlatten.BackgroundImage = Image.FromFile(tempFilePathToAssetDirectory + "Flatten.jpg");
            itSmooth.BackgroundImage = Image.FromFile(tempFilePathToAssetDirectory + "Smooth.jpg");
            itNoise.BackgroundImage = Image.FromFile(tempFilePathToAssetDirectory + "Noise.jpg");

            // Placeholder
            iTopGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Noise.jpg");
            iFrontGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Flatten.jpg");
            iRightGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Subtract.jpg");
            // Placeholder
        }
        #endregion

        #region Methods
        public void UpdateWindow()
        {
            RenderTarget2D tempTextureToConvert = GameFiles.EditorWorkAreaRenderTexture2D;

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);
            
            tempMemoryStream.Close();
            tempMemoryStream.Dispose();
            tempMemoryStream = null;

            string bla = iViews.SelectedTab.Name;

            iPerspectiveGraphic.Image = tempImageToUpdate;
            iTopGraphic.Image = tempImageToUpdate;
            iFrontGraphic.Image = tempImageToUpdate;
            iRightGraphic.Image = tempImageToUpdate;

            Invalidate();

            if (mIsHoveringOnEditor)
            {
             //   InputManager.Get().MousePositionOnWindow = new Vector2(MousePosition.X - this.Location.X - 16, MousePosition.Y - this.Location.Y -273);
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
        }

        void iGameGraphic_MouseClick(object sender, MouseEventArgs aMouseEvent)
        {
            Vector2 tempMousePosition = new Vector2(aMouseEvent.X, aMouseEvent.Y);
        }
        #endregion

        private void iPerspectiveGraphic_Click(object sender, MouseEventArgs e)
        {
         //   InputManager.Get().MousePositionOnWindow = new Vector2(e.X, e.Y);
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