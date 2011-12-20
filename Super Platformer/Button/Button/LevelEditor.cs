using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Button
{
    public class LevelEditor
    {
        /** Not actually used. Windows form uses the MVC design pattern. I wonder if I should keep this 'editor' code seperate, 
         * or shove it into the C[ontroller] part of the pattern. Meh... MVCC is the way to go. */

        #region Data
        private readonly Vector2 mTextureDimensions = new Vector2(256, 265);

        private Texture2D mTexture2D = null;
        private string mFilepath;

        private RenderTarget2D mRenderTarget2D;
        private List<EditorTexture2D> mTexturesToDraw = new List<EditorTexture2D>();
        private SpriteBatch mSpriteBatch = null;
        private GraphicsDevice mGraphicsDevice = null;

        private LevelEditorInterface mLevelEditorInterface = null;
        #endregion

        #region Construction
        public LevelEditor(string aFilepath, LevelEditorInterface aLevelEditorInterface)
        {
            mFilepath = aFilepath;
            mLevelEditorInterface = aLevelEditorInterface;

            Initialize();
        }

        public void Initialize()
        {
            mTexture2D = FileManager.Get().LoadTexture2D(@mFilepath);
            mRenderTarget2D = new RenderTarget2D(FileManager.Get().GraphicsDevice, (int)mTextureDimensions.X, (int)mTextureDimensions.Y);
            FileManager.Get().SelectedTextureForTextureEditor = mRenderTarget2D;
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

                Texture2D tempTextureToUpdate = Texture2D.FromStream(FileManager.Get().GraphicsDevice, tempMemoryStream);
                mTexture2D = tempTextureToUpdate;

                tempMemoryStream.Close();
                tempMemoryStream = null;

                mLevelEditorInterface.UpdateWindow();

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
