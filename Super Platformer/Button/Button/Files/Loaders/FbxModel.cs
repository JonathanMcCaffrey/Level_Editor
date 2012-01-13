using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;

namespace LevelEditor
{
    //<summary>
    // Custom Fbx Loading for dynamic asset loading at runtime,
    // to allows users to drag and drop new assets into the editor
    // in realtime. Only used for static assets.
    //</summary>
    public class FbxModel
    {
        #region Fields
        private Matrix m_IdenityMatrix = Matrix.Identity;
        private Matrix m_ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 10, 100000);
        private Matrix m_WorldMatrix = Matrix.Identity;

        private VertexBuffer m_VertexBuffer;
        private IndexBuffer m_IndexBuffer;

        private Vector3 m_HighestPointOnModel = Vector3.Zero;
        private Vector3 m_LowestPointOnModel = Vector3.Zero;

        private StreamReader m_StreamReader;
        private string m_RawData = "";

        private Effect m_Effect;

        private string m_PathToTexture;
        private Texture2D m_Texture;

        private Vector3[] m_PositionVertex;
        private int[] m_PositionIndex;
        private Vector3[] m_PositionData;

        private Vector3[] m_NormalData;

        private Vector2[] m_UvVertex;
        private int[] m_UvIndex;
        private Vector2[] m_UvData;

        private Vector3 m_Translation;
        private Vector3 m_Rotation;
        private Vector3 m_Scale;

        private VertexPositionNormalTexture[] m_FbxModel;
        private int[] m_FbxIndex;
        private VertexPositionColor[] m_CollisionBox;

        private int m_PositionVertexCount;
        private int m_PositionIndexCount;
        private int m_NormalDataCount;
        private int m_UvVertexCount;
        private int m_UvIndexCount;
        private int m_UvDataCount;

        #endregion

        #region Properties
        private Matrix ViewMatrix
        {
            get { return Matrix.CreateLookAt(new Vector3(1, 50, 0), Vector3.Zero, Vector3.Up); }
        }

        public Vector3 HighestPointOnModel
        {
            get { return m_HighestPointOnModel; }
        }
        public Vector3 LowestPontOnModel
        {
            get { return m_LowestPointOnModel; }
        }
        #endregion

        #region Construction
        public FbxModel(string a_FbxFilePath)
        {
            m_Effect = GameFiles.Effect;

            m_StreamReader = new StreamReader(a_FbxFilePath);

            while (!m_StreamReader.EndOfStream)
            {
                m_RawData = m_StreamReader.ReadLine();

                if (m_RawData.StartsWith("Texture:"))
                {
                    //TODO: Add a dynamic texture loader.
                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("RelativeFilename:"))
                        {
                            int valueAt = m_RawData.IndexOf('"');
                            m_RawData.Remove(0, valueAt);
                            m_RawData.Remove(m_RawData.Length);

                            m_PathToTexture = m_RawData;

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("Vertices:"))
                {
                    int valueAt = m_RawData.IndexOf('*');
                    m_RawData.Remove(0, valueAt);
                    m_RawData.Remove(m_RawData.Length);
                    m_RawData.Remove(m_RawData.Length);

                    m_PositionVertexCount = (int)System.Convert.ToDouble(m_RawData);
                    m_PositionVertex = new Vector3[m_PositionVertexCount / 3];

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("a:"))
                        {
                            valueAt = m_RawData.IndexOf(' ');
                            m_RawData.Remove(0, valueAt);

                            string[] buffer = new string[m_PositionVertexCount];

                            buffer = m_RawData.Split(',');

                            for (int loop = 0; loop < buffer.Length; loop += 3)
                            {
                                m_PositionVertex[loop / 3] = new Vector3(   (float)System.Convert.ToDouble(buffer[loop]),
                                                                            (float)System.Convert.ToDouble(buffer[loop + 1]),
                                                                            (float)System.Convert.ToDouble(buffer[loop + 2]));
                            }

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("PolygonVertexIndex:"))
                {
                    int valueAt = m_RawData.IndexOf('*');
                    m_RawData.Remove(0, valueAt);
                    m_RawData.Remove(m_RawData.Length);
                    m_RawData.Remove(m_RawData.Length);

                    m_PositionIndexCount = (int)System.Convert.ToDouble(m_RawData);
                    m_PositionIndex = new int[m_PositionIndexCount / 3];

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("a:"))
                        {
                            valueAt = m_RawData.IndexOf(' ');
                            m_RawData.Remove(0, valueAt);

                            string[] buffer = new string[m_PositionIndexCount];

                            buffer = m_RawData.Split(',');

                            for (int loop = 0; loop < buffer.Length; loop++)
                            {
                                m_PositionIndex[loop] = (int)System.Convert.ToDouble(buffer[loop]);
                            }

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("LayerElementNormal:"))
                {
                    int valueAt = m_RawData.IndexOf('*');
                    m_RawData.Remove(0, valueAt);
                    m_RawData.Remove(m_RawData.Length);
                    m_RawData.Remove(m_RawData.Length);

                    m_NormalDataCount = (int)System.Convert.ToDouble(m_RawData);
                    m_NormalData = new Vector3[m_NormalDataCount / 3];

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("a:"))
                        {
                            valueAt = m_RawData.IndexOf(' ');
                            m_RawData.Remove(0, valueAt);

                            string[] buffer = new string[m_NormalDataCount];

                            buffer = m_RawData.Split(',');

                            for (int loop = 0; loop < buffer.Length; loop += 3)
                            {
                                m_NormalData[loop / 3] = new Vector3(   (float)System.Convert.ToDouble(buffer[loop]), 
                                                                        (float)System.Convert.ToDouble(buffer[loop + 1]), 
                                                                        (float)System.Convert.ToDouble(buffer[loop + 2]));
                            }

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("LayerElementUv:"))
                {
                    int valueAt = m_RawData.IndexOf('*');
                    m_RawData.Remove(0, valueAt);
                    m_RawData.Remove(m_RawData.Length);
                    m_RawData.Remove(m_RawData.Length);

                    m_UvVertexCount = (int)System.Convert.ToDouble(m_RawData);
                    m_UvVertex = new Vector2[m_UvVertexCount / 2];

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("a:"))
                        {
                            valueAt = m_RawData.IndexOf(' ');
                            m_RawData.Remove(0, valueAt);

                            string[] buffer = new string[m_UvVertexCount];

                            buffer = m_RawData.Split(',');

                            for (int loop = 0; loop < buffer.Length; loop += 2)
                            {
                                m_UvVertex[loop / 2] = new Vector2( (float)System.Convert.ToDouble(buffer[loop]), 
                                                                    (float)System.Convert.ToDouble(buffer[loop + 1]));
                            }

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("UvIndex:"))
                {
                    int valueAt = m_RawData.IndexOf('*');
                    m_RawData.Remove(0, valueAt);
                    m_RawData.Remove(m_RawData.Length);
                    m_RawData.Remove(m_RawData.Length);

                    m_UvIndexCount = (int)System.Convert.ToDouble(m_RawData);
                    m_UvIndex = new int[m_UvIndexCount];

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        m_RawData = m_StreamReader.ReadLine();
                        if (m_RawData.StartsWith("a:"))
                        {
                            valueAt = m_RawData.IndexOf(' ');
                            m_RawData.Remove(0, valueAt);

                            string[] buffer = new string[m_UvIndexCount];

                            buffer = m_RawData.Split(',');

                            for (int loop = 0; loop < buffer.Length; loop++)
                            {
                                m_UvIndex[loop] = System.Convert.ToInt16(buffer[loop]);
                            }

                            isStillParsing = false;
                        }
                    }
                }
                if (m_RawData.StartsWith("P: \"Lcl Translation\""))
                {
                    m_RawData = m_RawData.Replace(" P: \"Lcl Translation\", \"Lcl Translation\", \"\", \"A\",", string.Empty);

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        string[] buffer = new string[3];

                        buffer = m_RawData.Split(',');

                        for (int loop = 0; loop < buffer.Length; loop += 2)
                        {
                            m_Translation= new Vector3( (float)System.Convert.ToDouble(buffer[loop]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 1]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 2]));
                        }

                        isStillParsing = false;
                    }
                }
                if (m_RawData.StartsWith("P: \"Lcl Rotation\""))
                {
                    m_RawData = m_RawData.Replace(" P: \"Lcl Rotation\", \"Lcl Rotation\", \"\", \"A\",", string.Empty);

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        string[] buffer = new string[3];

                        buffer = m_RawData.Split(',');

                        for (int loop = 0; loop < buffer.Length; loop += 2)
                        {
                            m_Rotation = new Vector3((float)System.Convert.ToDouble(buffer[loop]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 1]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 2]));
                        }

                        isStillParsing = false;
                    }
                }
                if (m_RawData.StartsWith("P: \"Lcl Scale\""))
                {
                    m_RawData = m_RawData.Replace(" P: \"Lcl Scale\", \"Lcl Scale\", \"\", \"A\",", string.Empty);

                    bool isStillParsing = true;
                    while (isStillParsing)
                    {
                        string[] buffer = new string[3];

                        buffer = m_RawData.Split(',');

                        for (int loop = 0; loop < buffer.Length; loop += 2)
                        {
                            m_Scale = new Vector3((float)System.Convert.ToDouble(buffer[loop]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 1]),
                                                        (float)System.Convert.ToDouble(buffer[loop + 2]));
                        }

                        isStillParsing = false;
                    }
                }
            }

            m_StreamReader.Close();


            for (uint loop = 0; loop < m_PositionData.Length; loop++)
            {
                if (m_PositionData[loop].X > m_HighestPointOnModel.X)
                {
                    m_HighestPointOnModel.X = m_PositionData[loop].X;
                }
                if (m_PositionData[loop].Y > m_HighestPointOnModel.Y)
                {
                    m_HighestPointOnModel.Y = m_PositionData[loop].Y;
                }
                if (m_PositionData[loop].Z > m_HighestPointOnModel.Z)
                {
                    m_HighestPointOnModel.Z = m_PositionData[loop].Z;
                }

                if (m_PositionData[loop].X < m_LowestPointOnModel.X)
                {
                    m_LowestPointOnModel.X = m_PositionData[loop].X;
                }
                if (m_PositionData[loop].Y < m_LowestPointOnModel.Y)
                {
                    m_LowestPointOnModel.Y = m_PositionData[loop].Y;
                }
                if (m_PositionData[loop].Z < m_LowestPointOnModel.Z)
                {
                    m_LowestPointOnModel.Z = m_PositionData[loop].Z;
                }
            }

            m_CollisionBox = new VertexPositionColor[30];

            Color temporaryColor = Color.Red;
            int temporaryInt = 0;

            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;

            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;


            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;

            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;


            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_LowestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;

            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_HighestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_HighestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;
            m_CollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(m_LowestPointOnModel.X, m_HighestPointOnModel.Y, m_LowestPointOnModel.Z), temporaryColor); temporaryInt++;

            m_PositionData = new Vector3[m_PositionIndexCount];
            for(int loop = 0; loop < m_PositionIndexCount; loop++)
            {
                m_PositionData[loop] = m_PositionVertex[m_PositionIndex[loop]];
            }

            m_UvData = new Vector2[m_UvIndexCount];
            for(int loop = 0; loop < m_UvIndexCount; loop++)
            {
                m_UvData[loop] = m_UvVertex[m_UvIndex[loop]];
            }

            for (int loop = 0; loop < m_FbxModel.Length; loop++)
            {
                m_FbxModel[loop] = new VertexPositionNormalTexture(m_PositionData[loop], m_NormalData[loop], m_UvData[loop]);
            }

            for (int loop = 0; loop < m_FbxIndex.Length; loop++)
            {
                m_FbxIndex[loop] = loop;
            }


            m_VertexBuffer = new VertexBuffer(GameFiles.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, m_FbxModel.Length, BufferUsage.WriteOnly);
            m_VertexBuffer.SetData(m_FbxModel);
            m_IndexBuffer = new IndexBuffer(GameFiles.GraphicsDevice, typeof(int), m_FbxIndex.Length, BufferUsage.None);
            m_IndexBuffer.SetData(m_FbxIndex);
        }
        #endregion

        #region Methods
        public void Draw(Tile aTile)
        {
            GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Matrix defaultMatrix =  Matrix.Identity *
                                    Matrix.CreateScale(m_Scale) *
                                    Matrix.CreateRotationX(m_Rotation.X) *
                                    Matrix.CreateRotationY(m_Rotation.Y) *
                                    Matrix.CreateRotationZ(m_Rotation.Z) *
                                    Matrix.CreateTranslation(m_Translation);

            foreach (EffectPass pass in m_Effect.CurrentTechnique.Passes)
            {
                m_Effect.CurrentTechnique = m_Effect.Techniques["Standard"];
                m_Effect.Parameters["xWorldViewProjectionMatrix"].SetValue(defaultMatrix * aTile.ScaleMatrix * aTile.RotationMatrix * aTile.WorldMatrix * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);

                m_Effect.Parameters["xColorMap"].SetValue(aTile.Graphic);

                //TODO: Update generic shader to take in Normals

                pass.Apply();

                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, m_FbxModel, 0, m_FbxModel.Length / 3);
            }
        }

        #region ToString
        public override string ToString()
        {
            return "FbxModel.cs";
        }
        #endregion
        #endregion
    }
}
