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
        #region Construction
        public TextureEditorInterface()
        {
            InitializeComponent();

            iTextureEditor.BackgroundImageLayout = ImageLayout.Center;
        }
        #endregion

        #region Methods
     

        public void UpdateWindow()
        {
            Texture2D tempTextureToConvert = FileManager.Get().SelectedTextureForTextureEditor;

            if (tempTextureToConvert == null)
            {
                throw new Exception(this.ToString() + "\n\rNo texture to draw\n\r");
            }

            MemoryStream tempMemoryStream = new MemoryStream();

            tempTextureToConvert.SaveAsPng(tempMemoryStream, tempTextureToConvert.Width, tempTextureToConvert.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Image tempImageToUpdate = System.Drawing.Bitmap.FromStream(tempMemoryStream);

            tempMemoryStream.Close();
            tempMemoryStream = null;

            iTextureEditor.BackgroundImage = tempImageToUpdate; 
        }

        private void TextureEditor_Load(object sender, EventArgs e)
        {

        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "TextureEditorInterface.cs";
        }
        #endregion

        private void iTextureEditor_Paint(object sender, MouseEventArgs e)
        {
            Vector2 tempMousePosition = new Vector2(e.X, e.Y);
            Console.Write(tempMousePosition);
            Console.WriteLine(" ");

        }
        #endregion
    }
}
