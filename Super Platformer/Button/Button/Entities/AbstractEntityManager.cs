using System;
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

namespace LevelEditor
{
    // This is Deprecated.
    public class AbstractEntityManager : DrawableGameComponent
    {
        #region Singletons
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