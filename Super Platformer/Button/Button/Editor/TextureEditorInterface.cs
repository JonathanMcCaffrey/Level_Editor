using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LevelEditor
{
    public partial class TextureEditorInterface : Form
    {
        //TODO: Add draw dragging.

        #region Data
        private TextureEditor mTextureEditor = null;
        public TextureEditor TextureEditor
        {
            set
            {
                mTextureEditor = value;
            }
        }

        private bool mIsDrawing = false;

        RenderTarget2D tempTextureToConvert;
        #endregion

        #region Construction
        public TextureEditorInterface()
        {
            this.TopMost = false;

            InitializeComponent();
            InitializeImages();
        }
        #endregion

        #region Methods
        public void UpdateWindow()
        {
            tempTextureToConvert = GameFiles.TextureEditorRenderTarget2D;

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

            tempMemoryStream.Close();
            tempMemoryStream = null;

            iTextureGraphic.Image = tempImageToUpdate;

            Invalidate();
            
            /*
            if (FileManager.Get().GizmoSelection != null && FileManager.Get().GizmoSelection.Count >= 1)
            {
                tempTextureToConvert = FileManager.Get().GizmoSelection[0].RenderTarget;

                MemoryStream tempMemoryStream = new MemoryStream();

                tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
                tempMemoryStream.Seek(0, SeekOrigin.Begin);

                Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

                tempMemoryStream.Close();
                tempMemoryStream = null;

                iTextureGraphic.Image = tempImageToUpdate;

                Invalidate();
            }
          */
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

            it0.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number0.jpg");
            it1.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number1.jpg");
            it2.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number2.jpg");
            it3.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number3.jpg");
            it4.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number4.jpg");
            it5.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number5.jpg");
            it6.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number6.jpg");
            it7.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number7.jpg");
            it8.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number8.jpg");
            it9.Image = Image.FromFile(tempFilePathToAssetDirectory + "Number9.jpg");

            itAddSolid.Image = Image.FromFile(tempFilePathToAssetDirectory + "AddSolid.jpg");
            itAddDiffuse.Image = Image.FromFile(tempFilePathToAssetDirectory + "AddDiffuse.jpg");
            itSubtractSolid.Image = Image.FromFile(tempFilePathToAssetDirectory + "SubtractSolid.jpg");
            itSubtractDiffuse.Image = Image.FromFile(tempFilePathToAssetDirectory + "SubtractDiffuse.jpg");
        }

        private void itOpen_Click(object aSender, EventArgs aEvent)
        {
            OpenFileDialog tempFileDialog = new OpenFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        private void itSave_Click(object aSender, EventArgs aEvent)
        {
            SaveFileDialog tempFileDialog = new SaveFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

        private void iTextureGraphic_Click(object aSender, MouseEventArgs aMouseEvent)
        {
            Vector2 tempMousePosition = new Vector2(aMouseEvent.X, aMouseEvent.Y);

            if (mTextureEditor == null)
            {
                Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mTextureEditor", "mTextureEditor", this.ToString());
            }
            else
            {
                UpdateWindow();
                mTextureEditor.AddTextureToStack(new EditorTexture2D(GameFiles.LoadTexture2D("Background"), tempMousePosition, Microsoft.Xna.Framework.Color.White));
            }

            EntityComponetManager.Get().Test();

        }

        //TODO: Use this instead of clickity clicks. Give all windows there own mouse coords from InputManager.
        void iTextureGraphic_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mIsDrawing = false;
        }

        void iTextureGraphic_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mIsDrawing = true;
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "TextureEditorInterface.cs";
        }
        #endregion

        private void itNew_Click(object sender, EventArgs e)
        {
            mTextureEditor.Texture2D = GameFiles.LoadTexture2D("TextureEditorTest");
            mTextureEditor.Reset();
        }
        #endregion

        private void iTextureGraphic_Click(object sender, EventArgs e)
        {

        }
    }
}
