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
    public class EnemyManager : AbstractEntityManager
    {
        #region Data
        private List<Enemy> mList = new List<Enemy>();
        public List<Enemy> List
        {
            get { return mList; }
        }

        private string mFilePathToGraphic = "Wooden";
        public Texture2D Graphic
        {
            get { return theFileManager.LoadTexture2D(mFilePathToGraphic); }
        }
        public string FilePathToGraphic
        {
            get { return mFilePathToGraphic; }
            set { mFilePathToGraphic = value; }
        }

        private bool isCollidable = true;
        public bool IsCollidable
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }
        #endregion

        #region Construction
        private EnemyManager(Game aGame)
            : base(aGame) { }
        static EnemyManager Instance;
        static public EnemyManager Get(Game aGame)
        {
            if (null == Instance)
            {
                Instance = new EnemyManager(aGame);

            }

            return Instance;
        }
        static public EnemyManager Get()
        {
            return Instance;
        }
        #endregion

        #region Methods
        public override void Update(GameTime aGameTime)
        {
            if (theInputManager.SingleKeyPressInput(Keys.S))
            {
                Clear();
            }

            for (int i = 0; i < mList.Count; i++)
            {
                List[i].Update();
            }
        }

        public override void Draw(GameTime aGameTime)
        {
            for (int loop = 0; loop < List.Count; loop++)
            {
                List[loop].Draw();
            }
        }

        public void Add(Enemy aEntity)
        {
            List.Add(aEntity);
        }

        public void Remove(Enemy aEntity)
        {
            List.Remove(aEntity);
        }

        public override void Clear()
        {
            List.Clear();
        }

        public override void Save(XmlWriter aXmlWriter)
        {
            aXmlWriter.WriteStartElement("EnemyDescription");
            aXmlWriter.WriteElementString("Count", List.Count.ToString());
            aXmlWriter.WriteEndElement();

            for (int loop = 0; loop < List.Count; loop++)
            {
                aXmlWriter.WriteStartElement("Enemy");
                aXmlWriter.WriteElementString("Model", List[loop].FilePathToModel);
                aXmlWriter.WriteElementString("Graphic", List[loop].FilePathToGraphic);
                aXmlWriter.WriteElementString("Position", List[loop].WorldPosition.ToString());
                aXmlWriter.WriteElementString("IsCollidable", List[loop].IsCollidable.ToString());
                aXmlWriter.WriteElementString("Color", List[loop].Color.ToString());
                aXmlWriter.WriteElementString("Rotation", List[loop].Rotation.ToString());
                aXmlWriter.WriteElementString("Scale", List[loop].Scale.ToString());
                aXmlWriter.WriteElementString("SpriteEffects", List[loop].SpriteEffects.ToString());
                aXmlWriter.WriteElementString("LayerDepth", List[loop].LayerDepth.ToString());
                aXmlWriter.WriteEndElement();
            }
        }

        public override void Load(string aFilePath)
        {
            using (XmlReader xmlReader = XmlReader.Create(aFilePath))
            {
                xmlReader.MoveToContent();

                xmlReader.ReadStartElement("Data");

                string rawData;
                string[] organizedData;

                string[] xData;
                string[] yData;
                string[] zData;

                xmlReader.ReadToFollowing("EnemyDescription");

                int count;
                xmlReader.ReadToFollowing("Count");
                count = xmlReader.ReadElementContentAsInt("Count", "");

                xmlReader.ReadToFollowing("Enemy");
                for (int loop = 0; loop < count; loop++)
                {
                    xmlReader.ReadStartElement("Enemy");

                    Enemy temporaryEnemy = new Enemy();

                    temporaryEnemy.FilePathToModel = xmlReader.ReadElementContentAsString("Model", "");
                    temporaryEnemy.FilePathToGraphic = xmlReader.ReadElementContentAsString("Graphic", "");

                    rawData = xmlReader.ReadElementContentAsString("Position", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    zData = organizedData[2].Split(':');
                    zData[1] = zData[1].TrimEnd();
                    zData[1] = zData[1].Replace('}', ' ');
                    temporaryEnemy.WorldPosition = new Vector3((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]), (float)Convert.ToDouble(zData[1]));

                    rawData = xmlReader.ReadElementContentAsString("IsCollidable", "");
                    if (rawData == "True")
                    {
                        temporaryEnemy.IsCollidable = true;
                    }
                    else
                    {
                        temporaryEnemy.IsCollidable = false;
                    }

                    rawData = xmlReader.ReadElementContentAsString("Color", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    zData = organizedData[2].Split(':');
                    zData[1] = zData[1].TrimEnd();
                    zData[1] = zData[1].Replace('}', ' ');
                    temporaryEnemy.Color = new Color((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]), (float)Convert.ToDouble(zData[1]));


                    rawData = xmlReader.ReadElementContentAsString("Rotation", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    zData = organizedData[2].Split(':');
                    zData[1] = zData[1].TrimEnd();
                    zData[1] = zData[1].Replace('}', ' ');
                    temporaryEnemy.Rotation = new Vector3((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]), (float)Convert.ToDouble(zData[1]));


                    rawData = xmlReader.ReadElementContentAsString("Scale", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    zData = organizedData[2].Split(':');
                    zData[1] = zData[1].TrimEnd();
                    zData[1] = zData[1].Replace('}', ' ');
                    temporaryEnemy.Scale = new Vector3((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]), (float)Convert.ToDouble(zData[1]));



                    switch (xmlReader.ReadElementContentAsString("SpriteEffects", ""))
                    {
                        case "FlipVertically":
                            temporaryEnemy.SpriteEffects = SpriteEffects.FlipVertically;
                            break;
                        case "FlipHorizontally":
                            temporaryEnemy.SpriteEffects = SpriteEffects.FlipHorizontally;
                            break;
                        case "None":
                            temporaryEnemy.SpriteEffects = SpriteEffects.None;
                            break;
                        default: break;
                    }

                    temporaryEnemy.LayerDepth = xmlReader.ReadElementContentAsFloat("LayerDepth", "");

                    Enemy.CreateEnemy(temporaryEnemy.WorldPosition);

                    EnemyTurret.CreateEnemy(temporaryEnemy.WorldPosition);

                    xmlReader.ReadEndElement();

                    Add(temporaryEnemy);
                }
                xmlReader.Close();
            }
        }

        public override string Statistic()
        {
            int temporaryStatistic = mList.Count;

            return "Total: " + temporaryStatistic.ToString();
        }
        #endregion
    }
}