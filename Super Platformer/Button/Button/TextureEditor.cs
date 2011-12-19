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
        private List<EditorTexture2D> mTexturesToDraw = null;
        private SpriteBatch mSpriteBatch = null;
        private GraphicsDevice mGraphicsDevice = null;

        private TextureEditorInterface mTextureEditorInterface = null;
        #endregion

        #region Construction
        public TextureEditor(string aFilepath, TextureEditorInterface aTextureEditorInterface)
        {
            mFilepath = aFilepath;
            mTextureEditorInterface = aTextureEditorInterface;

            Initialize();
        }

        public void Initialize()
        {
            mTexture2D = FileManager.Get().LoadTexture2D(@mFilepath);
            FileManager.Get().SelectedTextureForTextureEditor = mTexture2D;

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
                if (mTexturesToDraw != null)
                {
                    mTexturesToDraw.Clear();
                }
                mGraphicsDevice.SetRenderTarget(null);

                mTextureEditorInterface.UpdateWindow();
            }
        }
        #endregion

        #region Methods
        public void AddTextureToStack(EditorTexture2D aTexture2D)
        {
            mTexturesToDraw.Add(aTexture2D);
        }

        public void DrawIntoTextureEditor()
        {
            if (mTexturesToDraw != null)
            {
                mGraphicsDevice.SetRenderTarget(mRenderTarget2D);
                mSpriteBatch.Begin();

                for (int loop = 0; loop < mTexturesToDraw.Count; loop++)
                {
                    mSpriteBatch.Draw(mTexturesToDraw[loop].mTexture2D, mTexturesToDraw[loop].mPosition,
                        mTexturesToDraw[loop].mSourceRectangle, mTexturesToDraw[loop].mColor,
                        mTexturesToDraw[loop].mRotation, mTexturesToDraw[loop].mOrigin,
                         mTexturesToDraw[loop].mScale, mTexturesToDraw[loop].mSpriteEffect, 0);
                }

                mSpriteBatch.End();
                mTexturesToDraw.Clear();
                mGraphicsDevice.SetRenderTarget(null);

                mTextureEditorInterface.UpdateWindow();
            }
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
