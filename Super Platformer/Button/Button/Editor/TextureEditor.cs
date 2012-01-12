using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    //<summary>
    // Contains TextureEditor stuff.
    //</summary>
    public class TextureEditor
    {
        #region Data
        private readonly Vector2 mTextureDimensions = new Vector2(256, 256);

        private Texture2D mTexture2D = null;
        public Texture2D Texture2D
        {
            get { return mTexture2D; }
            set { mTexture2D = value; }
        }
        private string mFilepath;

        private RenderTarget2D mRenderTarget2D;
        private List<EditorTexture2D> mTexturesToDraw = new List<EditorTexture2D>();
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
            mTexture2D = GameFiles.LoadTexture2D(@mFilepath);
            mRenderTarget2D = new RenderTarget2D(GameFiles.GraphicsDevice, (int)mTextureDimensions.X, (int)mTextureDimensions.Y);
            GameFiles.TextureEditorRenderTarget2D = mRenderTarget2D;
            mSpriteBatch = GameFiles.SpriteBatch;
            mGraphicsDevice = GameFiles.GraphicsDevice;

       //     mGraphicsDevice.SetRenderTarget(mRenderTarget2D);
          //  mGraphicsDevice.Clear(Color.Black);

            if (mTexture2D == null)
            {
                Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mTexture2D", "mTexture2D", this.ToString());
            }
            else
            {
           /*     mSpriteBatch.Begin();
                mSpriteBatch.Draw(mTexture2D, Vector2.Zero, Color.White);
                mSpriteBatch.End();
                if (mTexturesToDraw != null)
                {
                    mTexturesToDraw.Clear();
                }
                mGraphicsDevice.SetRenderTarget(null);*/
            }

            mTextureEditorInterface.UpdateWindow();
        }
        #endregion

        #region Methods
        public void AddTextureToStack(EditorTexture2D aTexture2D)
        {
            mTexturesToDraw.Add(aTexture2D);
        }

        public void Reset()
        {
            mGraphicsDevice.SetRenderTarget(mRenderTarget2D);
            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mTexture2D, Vector2.Zero, Color.White);
            mSpriteBatch.End();
            mTexturesToDraw.Clear();

            mGraphicsDevice.SetRenderTarget(null);

            MemoryStream tempMemoryStream = new MemoryStream();

            mRenderTarget2D.SaveAsPng(tempMemoryStream, mRenderTarget2D.Width, mRenderTarget2D.Height);
            tempMemoryStream.Seek(0, SeekOrigin.Begin);

            Texture2D tempTextureToUpdate = Texture2D.FromStream(GameFiles.GraphicsDevice, tempMemoryStream);
            mTexture2D = tempTextureToUpdate;

            tempMemoryStream.Close();
            tempMemoryStream = null;

            mTextureEditorInterface.UpdateWindow();
        }

        public void DrawIntoTextureEditor()
        {
                if (mTexturesToDraw.Count > 0)
                {
                    mGraphicsDevice.SetRenderTarget(mRenderTarget2D);
                    mSpriteBatch.Begin();

                    mSpriteBatch.Draw(mTexture2D, Vector2.Zero, Color.White);

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

                    MemoryStream tempMemoryStream = new MemoryStream();

                    mRenderTarget2D.SaveAsPng(tempMemoryStream, mRenderTarget2D.Width, mRenderTarget2D.Height);
                    tempMemoryStream.Seek(0, SeekOrigin.Begin);

                    Texture2D tempTextureToUpdate = Texture2D.FromStream(GameFiles.GraphicsDevice, tempMemoryStream);
                    mTexture2D = tempTextureToUpdate;

                    tempMemoryStream.Close();
                    tempMemoryStream = null;

                    mTextureEditorInterface.UpdateWindow();

                    mTexturesToDraw.Clear();
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
