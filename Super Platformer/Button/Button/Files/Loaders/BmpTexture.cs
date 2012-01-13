using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Drawing;

namespace LevelEditor
{
    public class BmpTexture
    {
        #region Fields
        private string m_BmpFilePath = string.Empty;
        private Texture2D m_Texture = null;
        #endregion

        #region Properties
        public Texture2D Texture
        {
            get { return m_Texture; }
        }
        #endregion

        #region Construction
        public BmpTexture(string a_BmpFilePath)
        {
            m_BmpFilePath = a_BmpFilePath;

            Bitmap tempBitmap = new Bitmap(a_BmpFilePath);

            using (MemoryStream stream = new MemoryStream())
            {
                tempBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                m_Texture = Texture2D.FromStream(GameFiles.GraphicsDevice, stream);
            }
        }
        #endregion
    }
}
