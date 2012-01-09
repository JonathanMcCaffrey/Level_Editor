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
        private VertexPositionNormalTexture[] m_VextexData = new VertexPositionNormalTexture[65536];
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
                    m_VextexData[iterator].Position.X = xLoop * 8.0f + WorldPosition.X;
                    m_VextexData[iterator].Position.Z = yLoop * 8.0f + WorldPosition.Z;
                    m_VextexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    m_VextexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    m_VextexData[iterator].Position.Y = -m_ColorDataBuffer[iterator].R * 5.0f;

                    m_VextexData[iterator].Normal.X = 0.0f;
                    m_VextexData[iterator].Normal.Y = 1.0f;
                    m_VextexData[iterator].Normal.Z = 0.0f;

                    iterator++;
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
                    m_VextexData[iterator].Position.X = (xLoop * 8.0f) + WorldPosition.X;
                    m_VextexData[iterator].Position.Z = (yLoop * 8.0f) + WorldPosition.Z;
                    m_VextexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    m_VextexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    m_VextexData[iterator].Position.Y = -m_ColorDataBuffer[iterator].R * 5.0f;

                    m_VextexData[iterator].Normal.X = 0.0f;
                    m_VextexData[iterator].Normal.Y = 1.0f;
                    m_VextexData[iterator].Normal.Z = 0.0f;

                    iterator++;
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
                GameFiles.Effect.Parameters["xWorldViewProjectionMatrix"].SetValue( Matrix.Identity * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);
                GameFiles.Effect.Parameters["xColorMap"].SetValue(m_HeightMap);

                pass.Apply();

                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, m_VextexData, 0, m_VextexData.Length / 2);
            }
        }
        #endregion
    }
}