using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
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
        #endregion

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

            iGame.BackgroundImage = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Noise.jpg");
        }

        public void UpdateWindow()
        {
            RenderTarget2D tempTextureToConvert = FileManager.Get().SelectedTextureForTextureEditor;

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

            tempMemoryStream.Close();
            tempMemoryStream = null;

            iGame.BackgroundImage = tempImageToUpdate;

            Invalidate();
            /*
            if (isDrawing)
            {
                //TODO: Do this;

                 Vector2 tempMousePosition = new Vector2(e.X, e.Y);
            Console.Write(tempMousePosition);

            if (mTextureEditor == null)
            {
                throw new Exception(ToString() + "\n\rNo set Texture Editior\n\r");
            }
            else
            {
                mTextureEditor.AddTextureToStack(new EditorTexture2D(FileManager.Get().LoadTexture2D("MetalWall"), tempMousePosition, Microsoft.Xna.Framework.Color.White));
            }

            Console.WriteLine(" ");
            }*/
        }

        private void itOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempFileDialog = new OpenFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }

       private void itSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog tempFileDialog = new SaveFileDialog();
            tempFileDialog.ShowDialog();
            tempFileDialog.Dispose();
        }
    }
}
