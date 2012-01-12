using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    //<summary>
    // Contains WorldBox stuff.
    //</summary>
    public class WorldBox
    {
        #region Constants
        const int GRID_SIZE = 1024;
        #endregion

        #region Fields
        private Vector3 m_Dimensions;
        private Vector3 m_Origin = Vector3.Zero;

        private VertexPositionColor[] m_GridData;
        private VertexPositionColor[] m_BoxData;
        #endregion

        #region Property
        public Vector3 Dimensions
        {
            get { return m_Dimensions; }
            set { m_Dimensions = value; }
        }
        public float DimensionsX
        {
            get { return m_Dimensions.X; }
            set { m_Dimensions.X = value; }
        }
        public float DimensionsY
        {
            get { return m_Dimensions.Y; }
            set { m_Dimensions.Y = value; }
        }
        public float DimensionsZ
        {
            get { return m_Dimensions.Z; }
            set { m_Dimensions.Z = value; }
        }

        public Vector3 MaxPoint
        {
            get { return m_Origin + (m_Dimensions / 2); }
        }
        public int MaxPointX
        {
            get { return (int)(m_Origin + (m_Dimensions / 2)).X; }
        }
        public int MaxPointY
        {
            get { return (int)(m_Origin + (m_Dimensions / 2)).Y; }
        }
        public int MaxPointZ
        {
            get { return (int)(m_Origin + (m_Dimensions / 2)).Z; }
        }

        public Vector3 MinPoint
        {
            get { return m_Origin - (m_Dimensions / 2); }
        }
        public int MinPointX
        {
            get { return (int)(m_Origin - (m_Dimensions / 2)).X; }
        }
        public int MinPointY
        {
            get { return (int)(m_Origin - (m_Dimensions / 2)).Y; }
        }
        public int MinPointZ
        {
            get { return (int)(m_Origin - (m_Dimensions / 2)).Z; }
        }
        #endregion

        #region Construction
        public WorldBox() { }
        #endregion

        #region Methods
        public void Update()
        {
            int numberOfGridBoxes = (int)((DimensionsX / GRID_SIZE) * (DimensionsY / GRID_SIZE) * (DimensionsZ / GRID_SIZE));
            m_GridData = new VertexPositionColor[numberOfGridBoxes * 48];

            int iterator = 0;

            #region Wall of Text
            for (int xLoop = MinPointX; xLoop < MaxPointX; xLoop += GRID_SIZE)
            {
                for (int yLoop = MinPointY; yLoop < MaxPointY; yLoop += GRID_SIZE)
                {
                    for (int zLoop = MinPointZ; zLoop < MaxPointZ; zLoop += GRID_SIZE)
                    {
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;


                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;


                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;


                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;


                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;


                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        m_GridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        m_GridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                    }
                }
            }

            for (int loop = 0; loop < m_GridData.Length; loop++)
            {
                m_GridData[loop].Color = Color.LightGreen;
            }

            m_BoxData = new VertexPositionColor[30];

            iterator = 0;
            Color temporaryColor = Color.Red;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;


            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;


            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            m_BoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;

            #endregion
        }

        public void Draw()
        {
         /*   GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (EffectPass pass in GameFiles.BasicEffect.CurrentTechnique.Passes)
            {
               // GameFiles.BasicEffect.Alpha = 0.5f;
                GameFiles.BasicEffect.VertexColorEnabled = true;
                GameFiles.BasicEffect.World = Matrix.CreateScale(3) * Matrix.Identity;
                GameFiles.BasicEffect.View = GameFiles.ViewMatrix;
                GameFiles.BasicEffect.Projection = GameFiles.ProjectionMatrix;

                pass.Apply();
                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, m_GridData, 0, m_GridData.Length / 2);
            }

            foreach (EffectPass pass in GameFiles.BasicEffect.CurrentTechnique.Passes)
            {
                GameFiles.BasicEffect.VertexColorEnabled = true;
                GameFiles.BasicEffect.World = Matrix.CreateScale(3) * Matrix.Identity;
                GameFiles.BasicEffect.View = GameFiles.ViewMatrix;
                GameFiles.BasicEffect.Projection = GameFiles.ProjectionMatrix;

                pass.Apply();
                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, m_BoxData, 0, m_BoxData.Length - 1);
            }*/
        }
        #endregion
    }
}