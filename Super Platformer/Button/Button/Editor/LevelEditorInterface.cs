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
        #region Enums
        enum View
        {
            PERSPECTIVE,
            TOP,
            FRONT,
            RIGHT
        }
        #endregion

        public IntPtr WindowHandle;

        #region Fields
        private View mSelectedView = View.PERSPECTIVE;
        private bool mIsHoveringOnEditor = false;
        private bool mIsControlKeyHeldDown = false;
        private WorldBox mWorldBox = new WorldBox();

        private bool mIsMouseDown = false;
        private Vector2 mOldMousePosition = Vector2.Zero;
        private Vector2 mCurrentMousePosition = Vector2.Zero;

        public Microsoft.Xna.Framework.Rectangle mRectangle;

        private Image tempImageToUpdate = null;

        #endregion

        #region Properties
        public TabControl Views
        {
            get { return iViews; }
        }

        public WorldBox WorldBox
        {
            get { return mWorldBox; }
        }
        #endregion

        #region Construction
        public LevelEditorInterface()
        {
            InitializeComponent();
            InitializeImages();

            mWorldBox.DimensionsX = (float)iWorldDimensionX.Value;
            mWorldBox.DimensionsY = (float)iWorldDimensionY.Value;
            mWorldBox.DimensionsZ = (float)iWorldDimensionZ.Value;

            mWorldBox.Update();

            WindowHandle = panel7.Handle;
        }

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
           // iTopGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Noise.jpg");
           // iFrontGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Flatten.jpg");
           // iRightGraphic.Image = Image.FromFile(tempFilePathToAssetDirectory + "Subtract.jpg");
            // Placeholder
        }
        #endregion

        #region Methods

        int x = 0;
        public void UpdateWindow()
        {
            RenderTarget2D tempTextureToConvert = GameFiles.EditorWorkAreaRenderTexture2D;

            using (MemoryStream tempMemoryStream = new MemoryStream())
            {
                tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
                tempMemoryStream.Seek(0, SeekOrigin.Begin);

                tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);
            }

            iPerspectiveGraphic.Image = tempImageToUpdate;

            Invalidate();

            if (mIsMouseDown)
            {
                mRectangle.Width = (int)(mCurrentMousePosition.X - mOldMousePosition.X);
                mRectangle.Height = (int)(mCurrentMousePosition.Y - mOldMousePosition.Y);
            }

            if (mIsHoveringOnEditor)
            {
                //   InputManager.Get().MousePositionOnWindow = new Vector2(MousePosition.X - this.Location.X - 16, MousePosition.Y - this.Location.Y -273);
            }

        }

        bool WorldBoxHasChanged()
        {
            bool tempBoolean = false;


            return tempBoolean;
        }

        #region Icons

        private void Open_Click(object sender, EventArgs aEvent)
        {
            OpenFileDialog tempFileDialog = new OpenFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        private void Save_Click(object sender, EventArgs aEvent)
        {
            SaveFileDialog tempFileDialog = new SaveFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        private void New_Click(object sender, EventArgs e)
        {
            EntityComponetManager.Get().Clear();
        }

        private void Undo_Click(object sender, EventArgs e)
        {

        }

        private void Redo_Click(object sender, EventArgs e)
        {

        }

        private void Arrow_Click(object sender, EventArgs e)
        {

        }

        private void Translate_Click(object sender, EventArgs e)
        {

        }

        private void Rotate_Click(object sender, EventArgs e)
        {

        }

        private void Scale_Click(object sender, EventArgs e)
        {

        }

        private void ScaleLinear_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region HandleInput
        private void PerspectiveView_MouseClick(object sender, EventArgs e)
        {
            mSelectedView = View.PERSPECTIVE;

            mIsMouseDown = true;

            Console.WriteLine(mRectangle);

            for (int loop = 0; loop < TileManager.Get().List.Count; loop++)
            {
                if (TileManager.Get().List[loop].SelectionRectangle.Intersects(new Microsoft.Xna.Framework.Rectangle(mRectangle.X, mRectangle.Y, 1, 1)))
                {
                    GameFiles.GizmoSelection[0] = TileManager.Get().List[loop];
                }
            }
        }

        void iPerspectiveGraphic_MouseDown(object sender, MouseEventArgs a_MouseEvent)
        {
            mRectangle.X = a_MouseEvent.X;
            mRectangle.Y = a_MouseEvent.Y;

            mRectangle.Width = 0;
            mRectangle.Height = 0;

            GameFiles.CurrentTile.Clone();

            mIsMouseDown = true;

            mOldMousePosition.X = a_MouseEvent.X;
            mOldMousePosition.Y = a_MouseEvent.Y;
        }

        void iPerspectiveGraphic_MouseUp(object sender, System.Windows.Forms.MouseEventArgs a_MouseEvent)
        {
            mIsMouseDown = false;
        }

        void iPerspectiveGraphic_MouseMove(object sender, MouseEventArgs a_MouseEvent)
        {
            mCurrentMousePosition.X = a_MouseEvent.X;
            mCurrentMousePosition.Y = a_MouseEvent.Y;
        }

        private void TopView_MouseClick(object sender, EventArgs e)
        {

        }

        private void FrontView_MouseClick(object sender, EventArgs e)
        {

        }

        private void RightView_MouseClick(object sender, EventArgs e)
        {

        }
        #endregion
        void LevelEditorInterface_KeyDown(object sender, System.Windows.Forms.KeyEventArgs aKey)
        {
            switch (aKey.KeyCode)
            {
                case Keys.ControlKey:
                    mIsControlKeyHeldDown = true;
                    break;
                case Keys.Space:
                    GameFiles.CurrentTile.Clone();
                    break;
            }
        }

        void LevelEditorInterface_KeyUp(object sender, System.Windows.Forms.KeyEventArgs aKey)
        {
            switch (aKey.KeyCode)
            {
                case Keys.ControlKey:
                    mIsControlKeyHeldDown = false;
                    break;
            }
        }

        private void iWorldDimensionX_ValueChanged(object sender, EventArgs aEvent)
        {
            mWorldBox.DimensionsX = (float)iWorldDimensionX.Value;
            mWorldBox.Update();
        }
        private void iWorldDimensionY_ValueChanged(object sender, EventArgs aEvent)
        {
            mWorldBox.DimensionsY = (float)iWorldDimensionY.Value;
            mWorldBox.Update();
        }
        private void iWorldDimensionZ_ValueChanged(object sender, EventArgs aEvent)
        {
            mWorldBox.DimensionsZ = (float)iWorldDimensionZ.Value;
            mWorldBox.Update();
        }
        #endregion

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void perspectiveControl1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}