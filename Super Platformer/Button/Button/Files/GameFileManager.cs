using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LevelEditor
{
    // <summary>
    // Contains helper members for loading.
    // Stores generic variables.
    // </summary>
    public static class GameFileManager
    {
        //TODO: Doxygen everything.
        #region Constants
        const float CAMERA_ORTHO_DISTANCE = 5000.0f;
        const float CAMERA_DRAW_DISTANCE = 9000.0f;
        static Vector3 CAMERA_STARTING_POSITION = new Vector3(5000.0f, 500.0f, 0.0f);
        #endregion

        #region Fields
        private static bool mHasBeenIntialized = false;

        private static List<SpriteFont> mSpriteFontList = new List<SpriteFont>();
        private static List<string> mSpriteFontFilePathList = new List<string>();
        private static List<Model> mModelList = new List<Model>();
        private static List<string> mModelFilePathList = new List<string>();
        private static List<Texture2D> mTexture2DList = new List<Texture2D>();
        private static List<string> mTexture2DFilePathList = new List<string>();

        private static LevelEditorInterface mLevelEditorInterface = null;

        private static RenderTarget2D mTextureEditorRenderTarget2D = null;
        private static RenderTarget2D mEditorWorkAreaRenderTarget2D = null;
        private static RenderTarget2D mRenderTarget2D = null;

        private static BasicEffect mBasicEffect = null;
        private static Effect mEffect = null;
        private static SpriteBatch mSpriteBatch;
        private static SpriteFont mSpriteFont;
        private static Game mGame = null;
        private static ContentManager mContentManager = null;
        private static GraphicsDevice mGraphicsDevice = null;
        
        private static Vector2 mScreenCenter = Vector2.Zero;
        private static Vector3 mCameraPosition = Vector3.Zero;
                
        private static Matrix mViewMatrix = Matrix.Identity;
        private static Matrix mCameraViewMatrix = Matrix.Identity;
        private static Matrix mTopViewMatrix = Matrix.Identity;
        private static Matrix mFrontViewMatrix = Matrix.Identity;
        private static Matrix mRightViewMatrix = Matrix.Identity;
        private static Matrix mProjectionMatrix = Matrix.Identity;
        private static Matrix mPerspectiveProjectionMatrix = Matrix.Identity;
        private static Matrix mOrthographicProjectionMatrix = Matrix.Identity;
                
        private static List<Tile> mGizmoSelection = null;
        private static Tile mCurrentTile = null;
        #endregion

        #region Properties
        public static RenderTarget2D TextureEditorRenderTarget2D
        {
            get
            {
                if (mTextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mTextureEditorRenderTarget2D", "mTextureEditorRenderTarget2D", ToString());
                }

                return mTextureEditorRenderTarget2D;
            }
            set { mTextureEditorRenderTarget2D = value; }
        }


        public static RenderTarget2D EditorWorkAreaRenderTexture2D
        {
            get
            {
                if (mTextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mEditorWorkAreaRenderTarget2D", "mEditorWorkAreaRenderTarget2D", ToString());
                }

                return mEditorWorkAreaRenderTarget2D;
            }
            set { mEditorWorkAreaRenderTarget2D = value; }
        }


        public static RenderTarget2D RenderTarget2D
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


        public static BasicEffect BasicEffect
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


        public static Effect Effect
        {
            get
            {
                if (mEffect == null)
                {
                    string effectToLoad = "Basic";

                    try
                    {
                        mEffect = mContentManager.Load<Effect>("Basic");
                    }
                    catch
                    {
                        string warningMessage = string.Format("{0} does not exist.", effectToLoad);
                        throw new Exception(warningMessage);
                    }
                }

                return mEffect;
            }
        }

        public static SpriteBatch SpriteBatch
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

        public static SpriteFont SpriteFont
        {
            get
            {
                if (mSpriteFont == null)
                {
                    string spriteFontToLoad = "Title";

                    try
                    {
                        mSpriteFont = LoadFont(spriteFontToLoad);
                    }
                    catch
                    {
                        string warningMessage = string.Format("{0} does not exist.", spriteFontToLoad);
                        throw new Exception(warningMessage);
                    }
                }

                return mSpriteFont;

            }
            set { mSpriteFont = value; }
        }


        public static ContentManager ContentManager
        {
            get
            {
                if (mContentManager == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mContentManager", "GameFileManager", ToString());
                }

                return mContentManager;
            }
        }


        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                if (mGraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGraphicsDevice", "GameFileManager", ToString());
                }

                return mGraphicsDevice;
            }
        }


        public static Vector2 ScreenCenter
        {
            get
            {
                if (mGraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mScreenCenter", "GameFileManager", ToString());
                }

                if (mScreenCenter.Y == 0)
                {
                    mScreenCenter = new Vector2(mGraphicsDevice.Viewport.Width / 2, mGraphicsDevice.Viewport.Height / 2);
                }

                return mScreenCenter;
            }
        }

        /** PlaceHolder */
        // TODO: Move this somewhere it makes sense.

        public static Vector3 CameraPosition
        {
            get { return mCameraPosition; }
            set { mCameraPosition = value; }
        }

        public static Matrix ViewMatrix
        {
            get { return mViewMatrix; }
            set { mViewMatrix = value; }
        }

        public static Matrix CameraViewMatrix
        {
            get { return mCameraViewMatrix; }
            set { mCameraViewMatrix = value; }
        }

        public static Matrix TopViewMatrix
        {
            get { return mTopViewMatrix; }
            set { mTopViewMatrix = value; }
        }

        public static Matrix FrontViewMatrix
        {
            get { return mFrontViewMatrix; }
            set { mFrontViewMatrix = value; }
        }

        public static Matrix RightViewMatrix
        {
            get { return mRightViewMatrix; }
            set { mRightViewMatrix = value; }
        }

        public static Matrix ProjectionMatrix
        {
            get { return mProjectionMatrix; }
            set { mProjectionMatrix = value; }
        }

        public static Matrix PerspectiveProjectionMatrix
        {
            get { return mPerspectiveProjectionMatrix; }
            set { mPerspectiveProjectionMatrix = value; }
        }

        public static Matrix OrthographicProjectionMatrix
        {
            get { return mOrthographicProjectionMatrix; }
            set { mOrthographicProjectionMatrix = value; }
        }

        public static List<Tile> GizmoSelection
        {
            get
            {
                if (mGizmoSelection == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGizmoSelection", "mGizmoSelection", ToString());
                }

                return mGizmoSelection;
            }
            set { mGizmoSelection = value; }
        }


        public static Tile CurrentTile
        {
            get
            {
                if (mCurrentTile == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mCurrentTile", "mCurrentTile", ToString());
                }

                return mCurrentTile;
            }
            set { mCurrentTile = value; }
        }
        /** PlaceHolder */

        #endregion

        #region Methods
        public static void StartGameFileManager(Game aGame)
        {
            mHasBeenIntialized = true;

            mGame = aGame;
            mGraphicsDevice = aGame.GraphicsDevice;
            mContentManager = aGame.Content;

            mCameraPosition = CAMERA_STARTING_POSITION;

            mCameraViewMatrix = Matrix.CreateLookAt(CameraPosition, Vector3.Zero, Vector3.Up);
            mPerspectiveProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height, 1.0f, CAMERA_DRAW_DISTANCE);
            mOrthographicProjectionMatrix = Matrix.CreateOrthographic(2000, 2000, 1, CAMERA_DRAW_DISTANCE);

            mTopViewMatrix = Matrix.CreateLookAt(new Vector3(0, CAMERA_ORTHO_DISTANCE, 0), Vector3.Zero, Vector3.Right);
            mFrontViewMatrix = Matrix.CreateLookAt(new Vector3(-CAMERA_ORTHO_DISTANCE, 1, 1), Vector3.Zero, Vector3.Up);
            mRightViewMatrix = Matrix.CreateLookAt(new Vector3(1, 1, CAMERA_ORTHO_DISTANCE), Vector3.Zero, Vector3.Up);

            mProjectionMatrix = mPerspectiveProjectionMatrix;
            mViewMatrix = mCameraViewMatrix;
        }

        public static SpriteFont LoadFont(string aFilePath)
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
            mSpriteFontList.Add(mGame.Content.Load<SpriteFont>(aFilePath));

            return mSpriteFontList[i];
        }

        public static Model LoadModel(string aFilePath)
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
            mModelList.Add(mGame.Content.Load<Model>(aFilePath));

            return mModelList[i];
        }

        public static Texture2D LoadTexture2D(string aFilePath)
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
            mTexture2DList.Add(mGame.Content.Load<Texture2D>(aFilePath));

            return mTexture2DList[i];
        }

        #region Common .NET Overrides
        public static string ToString()
        {
            return "GameFileManager.cs";
        }
        #endregion
        #endregion
    }
}