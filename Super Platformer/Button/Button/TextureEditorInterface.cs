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
            RenderTarget2D tempTextureToConvert = FileManager.Get().SelectedTextureForTextureEditor;

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

            tempMemoryStream.Close();
            tempMemoryStream = null;

            iTextureEditor.BackgroundImage = tempImageToUpdate;

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

        private void TextureEditor_Load(object sender, EventArgs e)
        {

        }


        private void iTextureEditor_Paint(object sender, MouseEventArgs e)
        {
            Vector2 tempMousePosition = new Vector2(e.X, e.Y);

            if (mTextureEditor == null)
            {
                throw new Exception(ToString() + "\n\rNo set Texture Editior\n\r");
            }
            else
            {
                mTextureEditor.AddTextureToStack(new EditorTexture2D(FileManager.Get().LoadTexture2D("MetalWall"), tempMousePosition, Microsoft.Xna.Framework.Color.White));
            }
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

            it0.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number0.jpg");
            it1.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number1.jpg");
            it2.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number2.jpg");
            it3.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number3.jpg");
            it4.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number4.jpg");
            it5.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number5.jpg");
            it6.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number6.jpg");
            it7.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number7.jpg");
            it8.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number8.jpg");
            it9.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Number9.jpg");

            itAddSolid.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\AddSolid.jpg");
            itAddDiffuse.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\AddDiffuse.jpg");
            itSubtractSolid.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\SubtractSolid.jpg");
            itSubtractDiffuse.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\SubtractDiffuse.jpg");
        
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
