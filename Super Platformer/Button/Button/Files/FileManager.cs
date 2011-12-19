using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Button
{
    public class FileManager : DrawableGameComponent
    {
        #region Data
        private List<SpriteFont> mSpriteFontList = new List<SpriteFont>();
        private List<string> mSpriteFontFilePathList = new List<string>();

        private List<Model> mModelList = new List<Model>();
        private List<string> mModelFilePathList = new List<string>();

        private List<Texture2D> mTexture2DList = new List<Texture2D>();
        private List<string> mTexture2DFilePathList = new List<string>();

        private Texture2D mSelectedTextureForTextureEditor = null;
        public Texture2D SelectedTextureForTextureEditor
        {
            get { return mSelectedTextureForTextureEditor; }
            set { mSelectedTextureForTextureEditor = value; }
        }

        private RenderTarget2D mRenderTarget2D;
        public RenderTarget2D RenderTarget2D
        {
            get
            {
                if (mRenderTarget2D == null)
                {
                    mRenderTarget2D = new RenderTarget2D(mGraphicsDevice, mGraphicsDevice.Viewport.Width, mGraphicsDevice.Viewport.Height);
                }
                return mRenderTarget2D;
            }
            set { mRenderTarget2D = value; }
        }

        private BasicEffect mBasicEffect;
        public BasicEffect BasicEffect
        {
            get
            {
                if (mBasicEffect == null)
                {
                    mBasicEffect = new BasicEffect(mGraphicsDevice);
                }
                return mBasicEffect;
            }
        }

        private Effect mEffect;
        public Effect Effect
        {
            get
            {
                if (mEffect == null)
                {
                    mEffect = mContentManager.Load<Effect>(@"Shader\Standard");
                }
                return mEffect;
            }
        }

        private SpriteBatch mSpriteBatch;
        public SpriteBatch SpriteBatch
        {
            get
            {
                if (mSpriteBatch == null)
                {
                    mSpriteBatch = new SpriteBatch(GraphicsDevice);
                }

                return mSpriteBatch;
            }
        }

        private SpriteFont mSpriteFont;
        public SpriteFont SpriteFont
        {
            get {
                if (mSpriteFont == null)
                {
                    mSpriteFont = LoadFont("Title");
                }

                return mSpriteFont;
            
            }
            set { mSpriteFont = value; }
        }

        private ContentManager mContentManager;
        public ContentManager ContentManager
        {
            get { return mContentManager; }
        }

        private GraphicsDevice mGraphicsDevice;
        public new GraphicsDevice GraphicsDevice
        {
            get { return mGraphicsDevice; }
        }

        private Vector2 mScreenCenter;
        public Vector2 ScreenCenter
        {
            get
            {

                if (mScreenCenter.Y == 0)
                {
                    mScreenCenter = new Vector2(mGraphicsDevice.Viewport.Width / 2, mGraphicsDevice.Viewport.Height / 2);
                }

                return mScreenCenter;
            }
        }

        private Vector3 mCameraPosition = new Vector3(5000, 500, 0);

        public Vector3 CameraPosition
        {
            get { return mCameraPosition; }
            set { mCameraPosition = value; }
        }
        private Matrix mViewMatrix;
        public Matrix ViewMatrix
        {
            get { return mViewMatrix; }
            set { mViewMatrix = value; }
        }
        private Matrix mProjectionMatrix;
        public Matrix ProjectionMatrix
        {
            get { return mProjectionMatrix; }
            set { mProjectionMatrix = value; }
        }

        public List<Tile> GizmoSelection;

        #endregion

        #region Construction
        private FileManager(Game aGame)
            : base(aGame)
        {
            mGraphicsDevice = aGame.GraphicsDevice;
            mContentManager = Game.Content;

            mViewMatrix = Matrix.CreateLookAt(CameraPosition, Vector3.Zero, Vector3.Up);
            mProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height, 1f, 9000f);

        }
        static FileManager Instance;
        static public FileManager Get(Game aGame)
        {
            if (null == Instance)
            {
                Instance = new FileManager(aGame);
            }

            return Instance;
        }
        static public FileManager Get()
        {
            return Instance;
        }
        #endregion

        #region Methods
        public SpriteFont LoadFont(string aFilePath)
        {
            int i = 0;
            for (i = 0; i < mSpriteFontFilePathList.Count; i++)
            {
                if (aFilePath == mSpriteFontFilePathList[i])
                {
                    return mSpriteFontList[i];
                }
            }

            mSpriteFontFilePathList.Add(aFilePath);
            mSpriteFontList.Add(Game.Content.Load<SpriteFont>(aFilePath));

            return mSpriteFontList[i];
        }

        public Model LoadModel(string aFilePath)
        {
            int i = 0;
            for (i = 0; i < mModelFilePathList.Count; i++)
            {
                if (aFilePath == mModelFilePathList[i])
                {
                    return mModelList[i];
                }
            }

            mModelFilePathList.Add(aFilePath);
            mModelList.Add(Game.Content.Load<Model>(aFilePath));

            return mModelList[i];
        }

        public Texture2D LoadTexture2D(string aFilePath)
        {
            int i = 0;
            for (i = 0; i < mTexture2DFilePathList.Count; i++)
            {
                if (aFilePath == mTexture2DFilePathList[i])
                {
                    return mTexture2DList[i];
                }
            }

            mTexture2DFilePathList.Add(aFilePath);
            mTexture2DList.Add(Game.Content.Load<Texture2D>(aFilePath));

            return mTexture2DList[i];
        }

        public string Statistic()
        {
            int temporaryStatistic = mModelList.Count + mSpriteFontList.Count + mTexture2DList.Count + mTexture2DList.Count;

            return "Total Files: " + temporaryStatistic.ToString();
        }

        #endregion
    }
}