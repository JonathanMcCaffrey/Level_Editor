﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;

namespace Button
{
    /** All entities are a deprecated interface and will be replaced with the proper componet system when this level editior project
     * is transfered into the main project. */
    public class AbstractEntityManager : DrawableGameComponent
    {
        #region Singletons
        protected FileManager theFileManager = FileManager.Get();
        protected InputManager theInputManager = InputManager.Get();
        protected UtilityManager theUtilityManager = UtilityManager.Get();
        protected TileManager theTileManager = TileManager.Get();
        protected ScreenManager theScreenManager = ScreenManager.Get();
        #endregion

        #region Data
        public AbstractEntityManager Manager
        {
            get { return this; }
        }

        public virtual List<AbstractEntity> List
        {
            get { return null; }
        }
        #endregion

        #region Construction
        protected AbstractEntityManager(Game aGame)
            : base(aGame) { }
        static AbstractEntityManager Instance;
        static public AbstractEntityManager Get(Game aGame)
        {
            if (null == Instance)
            {
                Instance = new AbstractEntityManager(aGame);
            }

            return Instance;
        }
        static public AbstractEntityManager Get()
        {
            return Instance;
        }
        #endregion

        #region Methods
        public override void Update(GameTime aGameTime) { }
        public override void Draw(GameTime aGameTime) { }
        public virtual void Add(AbstractEntity aEntity) { }
        public virtual void Remove(AbstractEntity aEntity) { }
        public virtual void Clear() { }
        public virtual void Generate(Vector3 aCoordinate) { }
        public virtual string Statistic() { return "Hi"; }

        public virtual void Save(XmlWriter aXmlWriter) { }
        public virtual void Load(string aFilePath) { }
        #endregion
    }
}