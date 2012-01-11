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
    // Custom Obj Loading for dynamic asset loading at runtime,
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
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("Vertices:"))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("PolygonVertexIndex:"))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("LayerElementNormal:"))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("LayerElementUv:"))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("UvIndex:"))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("\"Lcl Translation\""))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("\"Lcl Rotation\""))
                { 
                    // TODO: Finish the parsing logic.
                }
                if (m_RawData.StartsWith("\"Lcl Scale\""))
                { 
                    // TODO: Finish the parsing logic.
                }

                // TODO: Organize parsed data.
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


            int index = 0;
            for (int loop = 0; loop < m_FbxModel.Length; loop++)
            {
                // TODO: Set FbxModel
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


            foreach (EffectPass pass in m_Effect.CurrentTechnique.Passes)
            {
                m_Effect.CurrentTechnique = m_Effect.Techniques["Standard"];
                m_Effect.Parameters["xWorldViewProjectionMatrix"].SetValue(aTile.ScaleMatrix * aTile.RotationMatrix * aTile.WorldMatrix * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);

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
