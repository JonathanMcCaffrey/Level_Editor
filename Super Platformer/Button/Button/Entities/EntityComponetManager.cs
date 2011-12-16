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
        #endregion

        #region Data
        private bool mIsSaving = false;

        private SaveMap saveFile = new SaveMap();
        private LoadMap loadFile = new LoadMap();
        private Description assetDescription = new Description();
        private LevelEditorInterface levelEditor = new LevelEditorInterface();
        #endregion

        #region Construction
        List<AbstractEntityManager> mList = new List<AbstractEntityManager>();
        PlayerManager thePlayerManager;

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
            assetDescription.Visible = true;
            levelEditor.Visible = true;

            mList.Add(TileManager.Get());
            mList.Add(EnemyManager.Get());

            thePlayerManager = PlayerManager.Get();
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

                thePlayerManager.Update(aGameTime);
            }
        }

        public override void Draw(GameTime aGameTime)
        {
            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Draw(aGameTime);
            }

            thePlayerManager.Draw(aGameTime);
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