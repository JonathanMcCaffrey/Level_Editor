using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Button
{
    public class TextureEditor
    {
        #region Data
        private readonly Vector2 mTextureDimensions = new Vector2(256, 265);

        private Texture2D mTexture2D = null;
        private string mFilepath;

        private RenderTarget2D mRenderTarget2D = null;
        private List<Texture2D> mTexturesToDraw = null;
        private SpriteBatch mSpriteBatch = null;
        private GraphicsDevice mGraphicsDevice = null;


        #endregion

        #region Construction
        public TextureEditor(string aFilepath)
        {
            mFilepath = aFilepath;

            Initialize();
        }

        public void Initialize()
        {
            mTexture2D = FileManager.Get().LoadTexture2D(@mFilepath);

            mRenderTarget2D = new RenderTarget2D(FileManager.Get().GraphicsDevice, (int)mTextureDimensions.X, (int)mTextureDimensions.Y);
            mSpriteBatch = FileManager.Get().SpriteBatch;
            mGraphicsDevice = FileManager.Get().GraphicsDevice;

            mGraphicsDevice.SetRenderTarget(mRenderTarget2D);

            mGraphicsDevice.Clear(Color.Black);

            if (mTexture2D == null)
            {
                throw new Exception(this.ToString() + "No Starting Texture for Editing");
            }
            else
            {
                mSpriteBatch.Begin();
                mSpriteBatch.Draw(mTexture2D, Vector2.Zero, Color.White);
                mSpriteBatch.End();
                mTexturesToDraw.Clear();
                mGraphicsDevice.SetRenderTarget(null);
            }
        }
        #endregion

        #region Methods
        public void DrawIntoTextureEditor()
        {
            mGraphicsDevice.SetRenderTarget(mRenderTarget2D);

            mSpriteBatch.Begin();
            if (mTexturesToDraw != null)
            {
                for (int loop = 0; loop < mTexturesToDraw.Count; loop++)
                {
                    mSpriteBatch.Draw(mTexturesToDraw[loop], Vector2.Zero, mTexturesToDraw[loop].Bounds, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
            mSpriteBatch.End();

            mTexturesToDraw.Clear();

            mGraphicsDevice.SetRenderTarget(null);
        }

        public void SaveTexture(string aFilePath)
        {
            Stream tempSaveSteam = File.OpenWrite(@aFilePath);

            mRenderTarget2D.SaveAsPng(tempSaveSteam, (int)mTextureDimensions.X, (int)mTextureDimensions.Y);
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "TextureEditor.cs";
        }
        #endregion
        #endregion
    }
}
