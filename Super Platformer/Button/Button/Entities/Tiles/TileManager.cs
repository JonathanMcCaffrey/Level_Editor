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
    public class TileManager : AbstractEntityManager
    {
        #region Data
        private List<Tile> mList = new List<Tile>();
        public List<Tile> List
        {
            get { return mList; }
        }
        #endregion

        #region Construction
        private TileManager(Game aGame)
            : base(aGame) { }
        static TileManager Instance;
        static public TileManager Get(Game aGame)
        {
            if (null == Instance)
            {
                Instance = new TileManager(aGame);

            }

            return Instance;
        }
        static public TileManager Get()
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

        public override void Add(AbstractEntity aEntity)
        {
            List.Add(aEntity as Tile);
        }

        public override void Remove(AbstractEntity aEntity)
        {
            List.Remove(aEntity as Tile);
        }

        public override void Clear()
        {
            List.Clear();
        }

        public override void Generate(Vector2 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
        }

        public override void Save(XmlWriter aXmlWriter)
        {
            aXmlWriter.WriteStartElement("TileDescription");
            aXmlWriter.WriteElementString("Count", List.Count.ToString());
            aXmlWriter.WriteEndElement();

            for (int loop = 0; loop < List.Count; loop++)
            {
                aXmlWriter.WriteStartElement("Tile");
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

                string rawData;
                string[] organizedData;

                string[] xData;
                string[] yData;
                string[] zData;

                xmlReader.ReadToFollowing("TileDescription");

                int count;
                xmlReader.ReadToFollowing("Count");
                count = xmlReader.ReadElementContentAsInt("Count", "");

                xmlReader.ReadToFollowing("Tile");
                for (int loop = 0; loop < count; loop++)
                {
                    xmlReader.ReadStartElement("Tile");

                    Tile temporaryTile = new Tile();

                    temporaryTile.FilePathToGraphic = xmlReader.ReadElementContentAsString("Graphic", "");

                    rawData = xmlReader.ReadElementContentAsString("Position", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    yData[1] = yData[1].TrimEnd();  // Glitch in C#: This will not work at this instance. Wonder why...
                    yData[1] = yData[1].Replace('}', ' ');  // Here is another solution.
                    temporaryTile.WorldPosition = new Vector2((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]));

                    rawData = xmlReader.ReadElementContentAsString("IsCollidable", "");
                    if (rawData == "True")
                    {
                        temporaryTile.IsCollidable = true;
                    }
                    else
                    {
                        temporaryTile.IsCollidable = false;
                    }

                    rawData = xmlReader.ReadElementContentAsString("Color", "");
                    organizedData = rawData.Split(' ');
                    xData = organizedData[0].Split(':');
                    yData = organizedData[1].Split(':');
                    zData = organizedData[2].Split(':');
                    zData[1] = zData[1].TrimEnd();
                    temporaryTile.Color = new Color((float)Convert.ToDouble(xData[1]), (float)Convert.ToDouble(yData[1]), (float)Convert.ToDouble(zData[1]));

                    temporaryTile.Rotation = xmlReader.ReadElementContentAsFloat("Rotation", "");

                    temporaryTile.Scale = xmlReader.ReadElementContentAsFloat("Scale", "");

                    switch (xmlReader.ReadElementContentAsString("SpriteEffects", ""))
                    {
                        case "FlipVertically":
                            temporaryTile.SpriteEffects = SpriteEffects.FlipVertically;
                            break;
                        case "FlipHorizontally":
                            temporaryTile.SpriteEffects = SpriteEffects.FlipHorizontally;
                            break;
                        case "None":
                            temporaryTile.SpriteEffects = SpriteEffects.None;
                            break;
                        default: break;
                    }

                    temporaryTile.LayerDepth = xmlReader.ReadElementContentAsFloat("LayerDepth", "");

                    xmlReader.ReadEndElement();

                    Add(temporaryTile);
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