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
    public partial class TextureEditorInterface : Form
    {
        string mFilePath = "C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Noise.jpg";

        public TextureEditorInterface()
        {
            InitializeComponent();

            iTextureEditor.BackgroundImageLayout = ImageLayout.Center;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public void UpdateWindow()
        {
            iTextureEditor.BackgroundImage = Image.FromFile(mFilePath);
        }

        private void TextureEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
