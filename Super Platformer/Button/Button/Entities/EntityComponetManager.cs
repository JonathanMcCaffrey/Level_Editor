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

namespace Button
{
    public class EntityComponetManager : DrawableGameComponent
    {
        #region Singletons
        InputManager theInputManager = InputManager.Get();
        FileManager theFileManager = FileManager.Get();
        #endregion

        #region Data
        private LevelEditorInterface levelEditor = new LevelEditorInterface();
        private TextureEditorInterface textureEditorInterface = new TextureEditorInterface();
        private AssetsWindow assetsWindow = new AssetsWindow();

        private TextureEditor textureEditor;

        private SpriteBatch mSpriteBatch;
        private GraphicsDevice mGraphicDevice;

        private RenderTarget2D mEditorWorkAreaRenderTexture2D;

        private GizmoComponent gizmo;

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

            theFileManager = FileManager.Get();

            mSpriteBatch = theFileManager.SpriteBatch;
            mGraphicDevice = theFileManager.GraphicsDevice;

            mEditorWorkAreaRenderTexture2D = new RenderTarget2D(mGraphicDevice, 728, 561);

            theFileManager.EditorWorkAreaRenderTexture2D = mEditorWorkAreaRenderTexture2D;

            gizmo = new GizmoComponent(theFileManager.ContentManager, theFileManager.GraphicsDevice);
            gizmo.Initialize();

        }
        #endregion

        #region Methods
        public override void Update(GameTime aGameTime)
        {
            gizmo.HandleInput();
            gizmo.Update(aGameTime);

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Update(aGameTime);
            }
        }

        public override void Draw(GameTime aGameTime)
        {
            textureEditor.DrawIntoTextureEditor();

            mGraphicDevice.SetRenderTarget(mEditorWorkAreaRenderTexture2D);
            mSpriteBatch.Begin();
            mGraphicDevice.Clear(Color.Green);

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Draw(aGameTime);
            }
            mSpriteBatch.Draw(FileManager.Get().LoadTexture2D("Arrow"), Vector2.Zero, Color.White);

            ButtonManager.Get().Draw(aGameTime);

            gizmo.Draw3D();

            mSpriteBatch.End();

            mGraphicDevice.SetRenderTarget(null);

            levelEditor.UpdateWindow();
         
            theFileManager.EditorWorkAreaRenderTexture2D = mEditorWorkAreaRenderTexture2D;
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