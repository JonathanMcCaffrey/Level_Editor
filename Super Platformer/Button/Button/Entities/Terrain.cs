using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    //<summary>
    // Contains terrain stuff.
    //</summary>
    public class Terrain
    {
        #region Fields
        private Texture2D m_HeightMap;
        private VertexPositionNormalTexture[] m_RawVertexData = new VertexPositionNormalTexture[65536];
        private VertexPositionNormalTexture[,] m_SortedVertexData = new VertexPositionNormalTexture[256, 256];
        private VertexPositionNormalTexture[] m_VertexData = new VertexPositionNormalTexture[390150];

        private Color[] m_ColorDataBuffer = new Color[65536];

        private Vector3 m_WorldPosition = Vector3.Zero;
        #endregion

        #region Properties
        public Vector3 WorldPosition
        {
            get { return m_WorldPosition; }
            set { m_WorldPosition = value; }
        }
        #endregion

        #region Construction
        public Terrain()
        {
            m_HeightMap = GameFiles.LoadTexture2D("TextureEditorTest");
            m_HeightMap.GetData<Color>(m_ColorDataBuffer);

            int iterator = 0;
            for (float xLoop = 0; xLoop < 256; xLoop++)
            {
                for (float yLoop = 0; yLoop < 256; yLoop++)
                {
                    m_RawVertexData[iterator].Position.X = (xLoop * 8.0f) + WorldPosition.X;
                    m_RawVertexData[iterator].Position.Z = (yLoop * 8.0f) + WorldPosition.Z;
                    m_RawVertexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    m_RawVertexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    m_RawVertexData[iterator].Position.Y = -m_ColorDataBuffer[iterator].R * 5.0f;

                    m_RawVertexData[iterator].Normal.X = 0.0f;
                    m_RawVertexData[iterator].Normal.Y = 1.0f;
                    m_RawVertexData[iterator].Normal.Z = 0.0f;

                    iterator++;
                }
            }

            int xIterator = 0;
            int yIterator = 0;
            iterator = 0;
            for (int loop = 0; loop < 65536; loop++)
            {
                if (xIterator == 256)
                {
                    xIterator = 0;
                    yIterator++;
                }

                m_SortedVertexData[xIterator, yIterator] = m_RawVertexData[iterator];
                xIterator++;
                iterator++;
            }

            iterator = 0;
            for (int xLoop = 0; xLoop < 255; xLoop++)
            {
                for (int yLoop = 0; yLoop < 255; yLoop++)
                {
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop + 1]; iterator++;
                    
                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop + 1]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop + 1]; iterator++;
                }
            }
        }
        #endregion

        #region Methods
        public void Update()
        {
            m_HeightMap = GameFiles.TextureEditorRenderTarget2D;
            m_HeightMap.GetData<Color>(m_ColorDataBuffer);

            int iterator = 0;
            for (float xLoop = 0; xLoop < 256; xLoop++)
            {
                for (float yLoop = 0; yLoop < 256; yLoop++)
                {
                    m_RawVertexData[iterator].Position.X = (xLoop * 8.0f) + WorldPosition.X;
                    m_RawVertexData[iterator].Position.Z = (yLoop * 8.0f) + WorldPosition.Z;
                    m_RawVertexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    m_RawVertexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    m_RawVertexData[iterator].Position.Y = -m_ColorDataBuffer[iterator].R * 5.0f;

                    m_RawVertexData[iterator].Normal.X = 0.0f;
                    m_RawVertexData[iterator].Normal.Y = 1.0f;
                    m_RawVertexData[iterator].Normal.Z = 0.0f;

                    iterator++;
                }
            }

            int xIterator = 0;
            int yIterator = 0;
            iterator = 0;
            for (int loop = 0; loop < 65536; loop++)
            {
                if (xIterator == 256)
                {
                    xIterator = 0;
                    yIterator++;
                }

                m_SortedVertexData[xIterator, yIterator] = m_RawVertexData[iterator];
                xIterator++;
                iterator++;
            }

            iterator = 0;
            for (int xLoop = 0; xLoop < 255; xLoop++)
            {
                for (int yLoop = 0; yLoop < 255; yLoop++)
                {
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop + 1]; iterator++;

                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop + 1, yLoop + 1]; iterator++;
                    m_VertexData[iterator] = m_SortedVertexData[xLoop, yLoop + 1]; iterator++;
                }
            }
        }

        public void Draw()
        {


            GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;



            foreach (EffectPass pass in GameFiles.Effect.CurrentTechnique.Passes)
            {
                GameFiles.Effect.CurrentTechnique = GameFiles.Effect.Techniques["Standard"];
                GameFiles.Effect.Parameters["xWorldViewProjectionMatrix"].SetValue(Matrix.Identity * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);
                GameFiles.Effect.Parameters["xColorMap"].SetValue(m_HeightMap);

                GameFiles.BasicEffect.World = Matrix.Identity * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix;

                pass.Apply();

                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, m_VertexData, 0, m_VertexData.Length / 3);
            }
        }
        #endregion
    }
}