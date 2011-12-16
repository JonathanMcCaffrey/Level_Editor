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
    public partial class LevelEditorInterface : Form
    {
        public LevelEditorInterface()
        {
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

            iAssetPreview.Image = Image.FromFile("C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\PlaceholderAsset.jpg");
            
            iIconStrip.Location = new Point((int)InputManager.Get().mousePosition.X, (int)InputManager.Get().mousePosition.Y);

        }

        private void IconStrip_MouseDown(object sender, EventArgs e)
        {
            Point tempPoint = iIconStrip.Location;

            tempPoint.X += (int)InputManager.Get().mouseTranslation.X;
            tempPoint.Y += (int)InputManager.Get().mouseTranslation.Y;

            Console.WriteLine((int)InputManager.Get().mouseTranslation.X);


            iIconStrip.Location = tempPoint;
        }
    }
}
