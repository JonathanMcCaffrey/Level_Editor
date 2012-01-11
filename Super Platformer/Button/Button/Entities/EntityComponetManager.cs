using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LevelEditor
{
    // This is Deprecated.
    public class EntityComponetManager : DrawableGameComponent
    {
        #region Data
        private LevelEditorInterface levelEditor = new LevelEditorInterface();
        private TextureEditorInterface textureEditorInterface = new TextureEditorInterface();
        private AssetsWindow assetsWindow = new AssetsWindow();

        private TextureEditor textureEditor;

        private SpriteBatch mSpriteBatch;
        private GraphicsDevice mGraphicDevice;

        private RenderTarget2D mEditorWorkAreaRenderTexture2D;

        private GizmoComponent gizmo;

        Terrain[] mTerrain;

        Texture2D mSelectionBox;
        Rectangle mRectangle;
        #endregion

        #region Construction
        List<AbstractEntityManager> mList = new List<AbstractEntityManager>();

        protected EntityComponetManager(Game aGame)
            : base(aGame) { }
        static EntityComponetManager Instance;
        static public EntityComponetManager Get(Game aGame)
        {
            if (null == Instance)
            {
                Instance = new EntityComponetManager(aGame);
            }

            return Instance;
        }
        static public EntityComponetManager Get()
        {
            return Instance;
        }

        public override void Initialize()
        {
            textureEditor = new TextureEditor("TextureEditorTest", textureEditorInterface);
            textureEditorInterface.TextureEditor = textureEditor;

            levelEditor.Visible = true;
            textureEditorInterface.Visible = true;
            assetsWindow.Visible = true;

            mList.Add(TileManager.Get());

            mSpriteBatch = GameFiles.SpriteBatch;
            mGraphicDevice = GameFiles.GraphicsDevice;

            mEditorWorkAreaRenderTexture2D = new RenderTarget2D(mGraphicDevice, 728, 561);

            GameFiles.EditorWorkAreaRenderTexture2D = mEditorWorkAreaRenderTexture2D;

            gizmo = new GizmoComponent();

            mTerrain = new Terrain[4];

            for (int loop = 0; loop < mTerrain.Length; loop += 2)
            {
                mTerrain[loop] = new Terrain();
                mTerrain[loop].WorldPosition = new Vector3(2048 * (-loop / 2), 0, -2048);

                mTerrain[loop + 1] = new Terrain();
                mTerrain[loop + 1].WorldPosition = new Vector3(2048 * (-loop / 2), 0, 0);
            }


            for (int loop = 0; loop < mTerrain.Length; loop++)
            {
                mTerrain[loop].Update();
            }

            mSelectionBox = GameFiles.LoadTexture2D("Selection");
            mRectangle = new Rectangle(0, 0, mSelectionBox.Width, mSelectionBox.Height);

            levelEditor.mRectangle = mRectangle;
        }
        #endregion

        #region Methods

        //** Test Code
        public void Test()
        {
            for (int loop = 0; loop < mTerrain.Length; loop++)
            {
                mTerrain[loop].Update();
            }
        }
        //** Test Code

        public override void Update(GameTime aGameTime)
        {
            mRectangle = levelEditor.mRectangle;

            if (levelEditor.Views.SelectedTab.Name == "tabPerspective")
            {
                GameFiles.ProjectionMatrix = GameFiles.PerspectiveProjectionMatrix;
                GameFiles.ViewMatrix = GameFiles.CameraViewMatrix;
            }
            else if (levelEditor.Views.SelectedTab.Name == "tabTop")
            {
                GameFiles.ProjectionMatrix = GameFiles.OrthographicProjectionMatrix;
                GameFiles.ViewMatrix = GameFiles.TopViewMatrix;
            }
            else if (levelEditor.Views.SelectedTab.Name == "tabFront")
            {
                GameFiles.ProjectionMatrix = GameFiles.OrthographicProjectionMatrix;
                GameFiles.ViewMatrix = GameFiles.FrontViewMatrix;
            }
            else if (levelEditor.Views.SelectedTab.Name == "tabRight")
            {
                GameFiles.ProjectionMatrix = GameFiles.OrthographicProjectionMatrix;
                GameFiles.ViewMatrix = GameFiles.RightViewMatrix;
            }

            gizmo.Update(aGameTime);

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Update(aGameTime);
            }
        }

        public override void Draw(GameTime aGameTime)
        {
         /*   textureEditor.DrawIntoTextureEditor();

            mGraphicDevice.SetRenderTarget(mEditorWorkAreaRenderTexture2D);
            mSpriteBatch.Begin();
            mGraphicDevice.Clear(Color.Green);

            for (int loop = 0; loop < mTerrain.Length; loop++)
            {
                mTerrain[loop].Draw();
            }

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Draw(aGameTime);
            }
            mSpriteBatch.Draw(GameFiles.LoadTexture2D("Arrow"), Vector2.Zero, Color.White);

            gizmo.Draw3D();



            levelEditor.WorldBox.Draw();

            mSpriteBatch.End();

            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
            mSpriteBatch.Draw(mSelectionBox, mRectangle, Color.Red);
            mSpriteBatch.End();

            mGraphicDevice.SetRenderTarget(null);
            */
            mGraphicDevice.Clear(Color.Red);

            mSpriteBatch.Begin();
            for (int loop = 0; loop < mTerrain.Length; loop++)
            {
                mTerrain[loop].Draw();
            }

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Draw(aGameTime);
            }

            levelEditor.WorldBox.Draw();

            mSpriteBatch.End();
            /*

            mGraphicDevice.SetRenderTarget(null);

            levelEditor.UpdateWindow();

            GameFiles.EditorWorkAreaRenderTexture2D = mEditorWorkAreaRenderTexture2D;*/
        }

        public void Clear()
        {
            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Clear();
            }
        }

        public void SaveAll(string aFilePath)
        {
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(aFilePath))
                {
                    xmlWriter.WriteStartElement("Data");

                    for (int loop = 0; loop < mList.Count; loop++)
                    {
                        mList[loop].Save(xmlWriter);
                    }

                    xmlWriter.Close();
                }
            }
            catch
            {
                Console.WriteLine("Error occured in {0}. {1}", "SaveAll", this.ToString());
            }
        }

        public void LoadAll(string aFilePath)
        {
            try
            {
                for (int loop = 0; loop < mList.Count; loop++)
                {
                    mList[loop].Load(aFilePath);
                }
            }
            catch
            {
                Console.WriteLine("Error occured in {0}. {1}", "LoadAll", this.ToString());
            }
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "EntityComponetManager.cs";
        }
        #endregion
        #endregion
    }
}