﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LevelEditor
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

        private LevelEditorInterface mLevelEditorInterface = null;


        private RenderTarget2D mTextureEditorRenderTarget2D = null;
        public RenderTarget2D TextureEditorRenderTarget2D
        {
            get
            {
                if (mTextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mTextureEditorRenderTarget2D", "mTextureEditorRenderTarget2D", this.ToString());
                }

                return mTextureEditorRenderTarget2D;
            }
            set { mTextureEditorRenderTarget2D = value; }
        }

        /** This is game texture that the level editor uses for rendering
         * the game in its main work window.*/
        private RenderTarget2D mEditorWorkAreaRenderTarget2D = null;
        public RenderTarget2D EditorWorkAreaRenderTexture2D
        {
            get
            {
                if (mTextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mEditorWorkAreaRenderTarget2D", "mEditorWorkAreaRenderTarget2D", this.ToString());
                }

                return mEditorWorkAreaRenderTarget2D;
            }
            set { mEditorWorkAreaRenderTarget2D = value; }
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
                    mEffect = mContentManager.Load<Effect>(@"Basic");
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
            get
            {
                if (mSpriteFont == null)
                {
                    mSpriteFont = LoadFont("Title");
                }

                return mSpriteFont;

            }
            set { mSpriteFont = value; }
        }

        private ContentManager mContentManager = null;
        public ContentManager ContentManager
        {
            get
            {
                if (mContentManager == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mContentManager", "FileManager", this.ToString());
                }

                return mContentManager;
            }
        }

        private GraphicsDevice mGraphicsDevice = null;
        public new GraphicsDevice GraphicsDevice
        {
            get
            {
                if (mGraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGraphicsDevice", "FileManager", this.ToString());
                }

                return mGraphicsDevice;
            }
        }

        private Vector2 mScreenCenter;
        public Vector2 ScreenCenter
        {
            get
            {
                if (mGraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mScreenCenter", "FileManager", this.ToString());
                }

                if (mScreenCenter.Y == 0)
                {
                    mScreenCenter = new Vector2(mGraphicsDevice.Viewport.Width / 2, mGraphicsDevice.Viewport.Height / 2);
                }

                return mScreenCenter;
            }
        }

        /** PlaceHolder */
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
        private Matrix mCameraViewMatrix;
        public Matrix CameraViewMatrix
        {
            get { return mCameraViewMatrix; }
            set { mCameraViewMatrix = value; }
        }
        private Matrix mTopViewMatrix;
        public Matrix TopViewMatrix
        {
            get { return mTopViewMatrix; }
            set { mTopViewMatrix = value; }
        }
        private Matrix mFrontViewMatrix;
        public Matrix FrontViewMatrix
        {
            get { return mFrontViewMatrix; }
            set { mFrontViewMatrix = value; }
        }
        private Matrix mRightViewMatrix;
        public Matrix RightViewMatrix
        {
            get { return mRightViewMatrix; }
            set { mRightViewMatrix = value; }
        }
        private Matrix mProjectionMatrix;
        public Matrix ProjectionMatrix
        {
            get { return mProjectionMatrix; }
            set { mProjectionMatrix = value; }
        }
        private Matrix mPerspectiveProjectionMatrix;
        public Matrix PerspectiveProjectionMatrix
        {
            get { return mPerspectiveProjectionMatrix; }
            set { mPerspectiveProjectionMatrix = value; }
        }
        private Matrix mOrthographicProjectionMatrix;
        public Matrix OrthographicProjectionMatrix
        {
            get { return mOrthographicProjectionMatrix; }
            set { mOrthographicProjectionMatrix = value; }
        }
        /** PlaceHolder */

        private List<Tile> mGizmoSelection = null;
        public List<Tile> GizmoSelection
        {
            get
            {
                if (mGizmoSelection == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGizmoSelection", "mGizmoSelection", this.ToString());
                }

                return mGizmoSelection;
            }
            set { mGizmoSelection = value; }
        }

        private Tile mCurrentTile = null;
        public Tile CurrentTile
        {
            get
            {
                if (mCurrentTile == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mCurrentTile", "mCurrentTile", this.ToString());
                }

                return mCurrentTile;
            }
            set { mCurrentTile = value; }
        }

        #endregion

        #region Construction
        private FileManager(Game aGame)
            : base(aGame)
        {
            mGraphicsDevice = aGame.GraphicsDevice;
            mContentManager = Game.Content;

            mCameraViewMatrix = Matrix.CreateLookAt(CameraPosition, Vector3.Zero, Vector3.Up);
            mPerspectiveProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height, 1f, 9000f);
            mOrthographicProjectionMatrix = Matrix.CreateOrthographic(2000, 2000, 1, 9000.0f);

            float reallyFarAway = 5000.0f;
            mTopViewMatrix = Matrix.CreateLookAt(new Vector3(0, reallyFarAway, 0), Vector3.Zero, Vector3.Right);
            mFrontViewMatrix = Matrix.CreateLookAt(new Vector3(-reallyFarAway, 1, 1), Vector3.Zero, Vector3.Up);
            mRightViewMatrix = Matrix.CreateLookAt(new Vector3(1, 1, reallyFarAway), Vector3.Zero, Vector3.Up);


            mProjectionMatrix = mPerspectiveProjectionMatrix;
            mViewMatrix = mCameraViewMatrix;
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

        #region Common .NET Overrides
        public override string ToString()
        {
            return "FileManager.cs";
        }
        #endregion
        #endregion
    }
}