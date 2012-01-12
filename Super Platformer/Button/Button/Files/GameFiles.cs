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
    public static class GameFiles
    {
        //TODO: Doxygen everything.
        #region Constants
        const float CAMERA_ORTHO_DISTANCE = 5000.0f;
        const float CAMERA_DRAW_DISTANCE = 18000.0f;
        static Vector3 CAMERA_STARTING_POSITION = new Vector3(5000.0f, 500.0f, 0.0f);
        #endregion

        #region Fields
        private static bool m_HasBeenIntialized = false;

        private static List<SpriteFont> m_SpriteFontList = new List<SpriteFont>();
        private static List<string> m_SpriteFontFilePathList = new List<string>();
        private static List<Model> m_ModelList = new List<Model>();
        private static List<string> m_ModelFilePathList = new List<string>();
        private static List<Texture2D> m_Texture2DList = new List<Texture2D>();
        private static List<string> m_Texture2DFilePathList = new List<string>();

        private static LevelEditorInterface m_LevelEditorInterface = null;

        private static RenderTarget2D m_TextureEditorRenderTarget2D = null;
        private static RenderTarget2D m_EditorWorkAreaRenderTarget2D = null;
        private static RenderTarget2D m_RenderTarget2D = null;

        private static BasicEffect m_BasicEffect = null;
        private static Effect m_Effect = null;
        private static SpriteBatch m_SpriteBatch;
        private static SpriteFont m_SpriteFont;
        private static Game m_Game = null;
        private static ContentManager m_ContentManager = null;
        private static GraphicsDevice m_GraphicsDevice = null;
        
        private static Vector2 m_ScreenCenter = Vector2.Zero;
        private static Vector3 m_CameraPosition = Vector3.Zero;
                
        private static Matrix mViewMatrix = Matrix.Identity;
        private static Matrix mCameraViewMatrix = Matrix.Identity;
        private static Matrix mTopViewMatrix = Matrix.Identity;
        private static Matrix mFrontViewMatrix = Matrix.Identity;
        private static Matrix mRightViewMatrix = Matrix.Identity;
        private static Matrix mProjectionMatrix = Matrix.Identity;
        private static Matrix mPerspectiveProjectionMatrix = Matrix.Identity;
        private static Matrix mOrthographicProjectionMatrix = Matrix.Identity;
                
        private static List<Tile> m_GizmoSelection = null;
        private static Tile m_CurrentTile = null;
        #endregion

        #region Properties
        public static RenderTarget2D TextureEditorRenderTarget2D
        {
            get
            {
                if (m_TextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mTextureEditorRenderTarget2D", "mTextureEditorRenderTarget2D", ToString());
                }

                return m_TextureEditorRenderTarget2D;
            }
            set { m_TextureEditorRenderTarget2D = value; }
        }


        public static RenderTarget2D EditorWorkAreaRenderTexture2D
        {
            get
            {
                if (m_TextureEditorRenderTarget2D == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mEditorWorkAreaRenderTarget2D", "mEditorWorkAreaRenderTarget2D", ToString());
                }

                return m_EditorWorkAreaRenderTarget2D;
            }
            set { m_EditorWorkAreaRenderTarget2D = value; }
        }


        public static RenderTarget2D RenderTarget2D
        {
            get
            {
                if (m_RenderTarget2D == null)
                {
                    m_RenderTarget2D = new RenderTarget2D(m_GraphicsDevice, m_GraphicsDevice.Viewport.Width, m_GraphicsDevice.Viewport.Height);
                }

                return m_RenderTarget2D;
            }
            set { m_RenderTarget2D = value; }
        }


        public static BasicEffect BasicEffect
        {
            get
            {
                if (m_BasicEffect == null)
                {
                    m_BasicEffect = new BasicEffect(m_GraphicsDevice);
                }

                return m_BasicEffect;
            }

            set
            {
                m_BasicEffect = value;
            }

        }


        public static Effect Effect
        {
            get
            {
                if (m_Effect == null)
                {
                    string effectToLoad = "Basic";

                    try
                    {
                        m_Effect = m_ContentManager.Load<Effect>("Basic");
                    }
                    catch
                    {
                        string warningMessage = string.Format("{0} does not exist.", effectToLoad);
                      //  throw new Exception(warningMessage);
                    }
                }

                return m_Effect;
            }
        }

        public static SpriteBatch SpriteBatch
        {
            get
            {
                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(GraphicsDevice);
                }

                return  m_SpriteBatch;
            }
        }

        public static SpriteFont SpriteFont
        {
            get
            {
                if (m_SpriteFont == null)
                {
                    string spriteFontToLoad = "Title";

                    try
                    {
                        m_SpriteFont = LoadFont(spriteFontToLoad);
                    }
                    catch
                    {
                        string warningMessage = string.Format("{0} does not exist.", spriteFontToLoad);
                        throw new Exception(warningMessage);
                    }
                }

                return m_SpriteFont;

            }
            set { m_SpriteFont = value; }
        }


        public static ContentManager ContentManager
        {
            get
            {
                if (m_ContentManager == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mContentManager", "GameFileManager", ToString());
                }

                return m_ContentManager;
            }
        }


        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                if (m_GraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGraphicsDevice", "GameFileManager", ToString());
                }

                return  m_GraphicsDevice;
            }

            set
            {
                m_GraphicsDevice = value;
            }
        }


        public static Vector2 ScreenCenter
        {
            get
            {
                if (m_GraphicsDevice == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mScreenCenter", "GameFileManager", ToString());
                }

                if (m_ScreenCenter.Y == 0)
                {
                    m_ScreenCenter = new Vector2(m_GraphicsDevice.Viewport.Width / 2, m_GraphicsDevice.Viewport.Height / 2);
                }

                return m_ScreenCenter;
            }
        }

        /** PlaceHolder */
        // TODO: Move this somewhere it makes sense.

        public static Vector3 CameraPosition
        {
            get { return m_CameraPosition; }
            set { m_CameraPosition = value; }
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
                if (m_GizmoSelection == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mGizmoSelection", "mGizmoSelection", ToString());
                }

                return m_GizmoSelection;
            }
            set { m_GizmoSelection = value; }
        }


        public static Tile CurrentTile
        {
            get
            {
                if (m_CurrentTile == null)
                {
                    Console.WriteLine("{0} is being called before {1} is initialized. {2}.", "mCurrentTile", "mCurrentTile", ToString());
                }

                return m_CurrentTile;
            }
            set { m_CurrentTile = value; }
        }
        /** PlaceHolder */

        #endregion

        #region Methods
        public static void StartGameFileManager(Game aGame)
        {
            m_HasBeenIntialized = true;

            m_Game = aGame;
            m_GraphicsDevice = aGame.GraphicsDevice;
            m_ContentManager = aGame.Content;

            m_CameraPosition = CAMERA_STARTING_POSITION;

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
            for (i = 0; i < m_SpriteFontFilePathList.Count; i++)
            {
                if (aFilePath == m_SpriteFontFilePathList[i])
                {
                    return m_SpriteFontList[i];
                }
            }

            m_SpriteFontFilePathList.Add(aFilePath);
            m_SpriteFontList.Add(m_Game.Content.Load<SpriteFont>(aFilePath));

            return m_SpriteFontList[i];
        }

        public static Model LoadModel(string aFilePath)
        {
            int i = 0;
            for (i = 0; i < m_ModelFilePathList.Count; i++)
            {
                if (aFilePath == m_ModelFilePathList[i])
                {
                    return m_ModelList[i];
                }
            }

            m_ModelFilePathList.Add(aFilePath);
            m_ModelList.Add(m_Game.Content.Load<Model>(aFilePath));

            return m_ModelList[i];
        }

        public static Texture2D LoadTexture2D(string aFilePath)
        {
            int i = 0;
            for (i = 0; i < m_Texture2DFilePathList.Count; i++)
            {
                if (aFilePath == m_Texture2DFilePathList[i])
                {
                    return m_Texture2DList[i];
                }
            }

            m_Texture2DFilePathList.Add(aFilePath);
            m_Texture2DList.Add(m_Game.Content.Load<Texture2D>(aFilePath));

            return m_Texture2DList[i];
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