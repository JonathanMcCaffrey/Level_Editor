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

namespace Button
{
    public class EntityComponetManager : DrawableGameComponent
    {
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
            mList.Add(TileManager.Get());
            mList.Add(EnemyManager.Get());

            //    mList.Add(PlayerManager.Get());
        }
        #endregion

        #region Methods

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
            for (int loop = 0; loop < mList.Count; loop++)
            {
                mList[loop].Load(aFilePath);
            }
        }
        #endregion
    }
}