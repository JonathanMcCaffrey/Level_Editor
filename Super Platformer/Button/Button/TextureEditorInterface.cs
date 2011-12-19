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

namespace Button
{
    public partial class TextureEditorInterface : Form
    {
        #region Data
        private TextureEditor mTextureEditor = null;
        public TextureEditor TextureEditor
        {
            set
            {
                if (mTextureEditor == null)
                {
                    //      throw new Exception(ToString() + "\n\rNull texture editor\n\r");
                }
                mTextureEditor = value;
            }
        }
        #endregion

        #region Construction
        public TextureEditorInterface()
        {
            InitializeComponent();
            InitializeImages();
        }
        #endregion

        #region Methods


        public void UpdateWindow()
        {
            RenderTarget2D tempTextureToConvert = FileManager.Get().SelectedTextureForTextureEditor;

            if (tempTextureToConvert == null)
            {
                //      throw new Exception(this.ToString() + "\n\rNo texture to draw\n\r");
            }

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

            tempMemoryStream.Close();
            tempMemoryStream = null;

            iTextureEditor.BackgroundImage = tempImageToUpdate;

            Invalidate();
        }

        private void TextureEditor_Load(object sender, EventArgs e)
        {

        }


        private void iTextureEditor_Paint(object sender, MouseEventArgs e)
        {
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

        }

        private void InitializeImages()
        {
            iTextureEditor.BackgroundImageLayout = ImageLayout.Center;

            itNew.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\New.jpg");
            itOpen.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Open.jpg");
            itSave.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Save.jpg");
            itUndo.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Undo.jpg");
            itRedo.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Redo.jpg");
            itArrow.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Arrow.jpg");
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

        #region Common .NET Overrides
        public override string ToString()
        {
            return "TextureEditorInterface.cs";
        }
        #endregion
        #endregion
    }
}
