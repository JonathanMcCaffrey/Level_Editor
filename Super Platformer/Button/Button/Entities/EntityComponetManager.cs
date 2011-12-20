﻿using System;
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
        private SaveMap saveFile = new SaveMap();
        private LoadMap loadFile = new LoadMap();
        private LevelEditorInterface levelEditor = new LevelEditorInterface();
        private TextureEditorInterface textureEditorInterface = new TextureEditorInterface();

        private TextureEditor textureEditor;

        private SpriteBatch mSpriteBatch;
        private GraphicsDevice mGraphicDevice;

        private RenderTarget2D mEditorWorkAreaRenderTexture2D;

        



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

            mList.Add(TileManager.Get());

            theFileManager = FileManager.Get();

            mSpriteBatch = theFileManager.SpriteBatch;
            mGraphicDevice = theFileManager.GraphicsDevice;

            mEditorWorkAreaRenderTexture2D = new RenderTarget2D(mGraphicDevice, 500, 500);

            theFileManager.EditorWorkAreaRenderTexture2D = mEditorWorkAreaRenderTexture2D;

        //    mGraphicDevice.SetRenderTarget(theFileManager.EditorWorkAreaRenderTexture2D);

        }
        #endregion

        #region Methods
        public override void Update(GameTime aGameTime)
        {

            if (saveFile.Done)
            {
                SaveAll(saveFile.FileName);
                saveFile.Done = false;
            }
            if (loadFile.Done)
            {
                LoadAll(loadFile.FileName);
                loadFile.Done = false;
            }

            if (theInputManager.SingleKeyPressInput(Keys.A) && loadFile.IsAccessible == false)
            {
                saveFile.On();
            }

            if (theInputManager.SingleKeyPressInput(Keys.D) && saveFile.IsAccessible == false)
            {
                loadFile.On();
            }

            if (!saveFile.IsAccessible && !loadFile.IsAccessible)
            {
                for (int loop = 0; loop < mList.Count; loop++)
                {
                    mList[loop].Update(aGameTime);
                }
            }
        }

        public override void Draw(GameTime aGameTime)
        {
            textureEditor.DrawIntoTextureEditor();

            mGraphicDevice.SetRenderTarget(theFileManager.EditorWorkAreaRenderTexture2D);

            mSpriteBatch.Begin();
           
            mGraphicDevice.Clear(Color.Green);

            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Draw(aGameTime);
            }
            mSpriteBatch.End();

            mGraphicDevice.SetRenderTarget(null);

         
        }

        public void SaveAll(string aFilePath)
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
                Console.WriteLine("Could not load");
            }
        }
        #endregion
    }
}