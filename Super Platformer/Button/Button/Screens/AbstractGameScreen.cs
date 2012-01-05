using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    // This is Deprecated.
    public abstract class AbstractGameScreen
    {
        #region Singletons
        protected TileManager theTileManager = TileManager.Get();
        protected ScreenManager theScreenManager = ScreenManager.Get();
        #endregion

        #region Data
        private Texture2D mBackgroundTexture;
        public Texture2D BackgroundTexture
        {
            get { return mBackgroundTexture; }
            set { mBackgroundTexture = value; }
        }

        private SpriteBatch mSpriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return mSpriteBatch; }
        }

        private GraphicsDevice mGraphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get { return mGraphicsDevice; }
        }

        private Color mBackgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return mBackgroundColor; }
            set { mBackgroundColor = value; }
        }

        #endregion

        #region Construction
        public AbstractGameScreen()
        {
            mSpriteBatch = GameFiles.SpriteBatch;
            mGraphicsDevice = GameFiles.GraphicsDevice;

            mBackgroundTexture = GameFiles.LoadTexture2D(@"Background");
        }
        #endregion

        #region Methods
        protected virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime aGameTime)
        {
        }
        public virtual void Draw(GameTime aGameTime)
        {
        }
        #endregion
    }
}
