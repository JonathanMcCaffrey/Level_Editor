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
        private Vector3 mDimensions;
        private Vector3 mOrigin = Vector3.Zero;

        private VertexPositionColor[] mGridData;
        private VertexPositionColor[] mBoxData;
        #endregion

        #region Property
        public Vector3 Dimensions
        {
            get { return mDimensions; }
            set { mDimensions = value; }
        }
        public float DimensionsX
        {
            get { return mDimensions.X; }
            set { mDimensions.X = value; }
        }
        public float DimensionsY
        {
            get { return mDimensions.Y; }
            set { mDimensions.Y = value; }
        }
        public float DimensionsZ
        {
            get { return mDimensions.Z; }
            set { mDimensions.Z = value; }
        }

        public Vector3 MaxPoint
        {
            get { return mOrigin + (mDimensions / 2); }
        }
        public int MaxPointX
        {
            get { return (int)(mOrigin + (mDimensions / 2)).X; }
        }
        public int MaxPointY
        {
            get { return (int)(mOrigin + (mDimensions / 2)).Y; }
        }
        public int MaxPointZ
        {
            get { return (int)(mOrigin + (mDimensions / 2)).Z; }
        }

        public Vector3 MinPoint
        {
            get { return mOrigin - (mDimensions / 2); }
        }
        public int MinPointX
        {
            get { return (int)(mOrigin - (mDimensions / 2)).X; }
        }
        public int MinPointY
        {
            get { return (int)(mOrigin - (mDimensions / 2)).Y; }
        }
        public int MinPointZ
        {
            get { return (int)(mOrigin - (mDimensions / 2)).Z; }
        }
        #endregion

        #region Construction
        public WorldBox() { }
        #endregion

        #region Methods
        public void Update()
        {
            int numberOfGridBoxes = (int)((DimensionsX / GRID_SIZE) * (DimensionsY / GRID_SIZE) * (DimensionsZ / GRID_SIZE));
            mGridData = new VertexPositionColor[numberOfGridBoxes * 30];

            int iterator = 0;

            for (int xLoop = MinPointX; xLoop < MaxPointX; xLoop += GRID_SIZE)
            {
                for (int yLoop = MinPointY; yLoop < MaxPointY; yLoop += GRID_SIZE)
                {
                    for (int zLoop = MinPointZ; zLoop < MaxPointZ; zLoop += GRID_SIZE)
                    {
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;


                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;


                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop - GRID_SIZE, zLoop - GRID_SIZE); iterator++;

                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop + GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop + GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                        mGridData[iterator].Position = new Vector3(xLoop - GRID_SIZE, yLoop + GRID_SIZE, zLoop - GRID_SIZE); iterator++;
                    }
                }
            }

            for (int loop = 0; loop < mGridData.Length; loop++)
            {
                mGridData[loop].Color = Color.LightGreen;
            }

            mBoxData = new VertexPositionColor[30];

            iterator = 0;
            Color temporaryColor = Color.Red;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;


            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;


            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MinPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MinPointY, MinPointZ), temporaryColor); iterator++;

            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MaxPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MaxPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
            mBoxData[iterator] = new VertexPositionColor(new Vector3(MinPointX, MaxPointY, MinPointZ), temporaryColor); iterator++;
        }

        public void Draw()
        {
            GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (EffectPass pass in GameFiles.BasicEffect.CurrentTechnique.Passes)
            {
                GameFiles.BasicEffect.Alpha = 0.5f;
                GameFiles.BasicEffect.VertexColorEnabled = true;
                GameFiles.BasicEffect.World = Matrix.CreateScale(3) * Matrix.Identity;
                GameFiles.BasicEffect.View = GameFiles.ViewMatrix;
                GameFiles.BasicEffect.Projection = GameFiles.ProjectionMatrix;

                pass.Apply();
                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mGridData, 0, mGridData.Length / 2);
            }

            foreach (EffectPass pass in GameFiles.BasicEffect.CurrentTechnique.Passes)
            {
                GameFiles.BasicEffect.VertexColorEnabled = true;
                GameFiles.BasicEffect.World = Matrix.CreateScale(3) * Matrix.Identity;
                GameFiles.BasicEffect.View = GameFiles.ViewMatrix;
                GameFiles.BasicEffect.Projection = GameFiles.ProjectionMatrix;

                pass.Apply();
                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, mBoxData, 0, mBoxData.Length - 1);
            }
        }
        #endregion
    }
}