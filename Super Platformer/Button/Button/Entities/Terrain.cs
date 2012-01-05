using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    class Terrain
    {
        #region Fields
        private Texture2D mHeightMap;
        private VertexPositionNormalTexture[] mVextexData = new VertexPositionNormalTexture[65536];
        private Color[] colorDataBuffer = new Color[65536];
        #endregion

        #region Construction
        public Terrain()
        {
            mHeightMap = GameFileManager.LoadTexture2D("TextureEditorTest");

            mHeightMap.GetData<Color>(colorDataBuffer);

            int iterator = 0;
            for (float xLoop = 0; xLoop < 256; xLoop++)
            {
                for (float yLoop = 0; yLoop < 256; yLoop++)
                {
                    //TODO: Add a sorting algorithm, or load a obj that happens to be the correct size.

                    mVextexData[iterator ].Position.X = xLoop * 2;
                    mVextexData[iterator].Position.Z = yLoop * 2;
                    mVextexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    mVextexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    mVextexData[iterator].Position.Y = -colorDataBuffer[iterator].R;

                    mVextexData[iterator].Normal.X = 0.0f;
                    mVextexData[iterator].Normal.Y = 1.0f;
                    mVextexData[iterator].Normal.Z = 0.0f;

                    iterator++;
                }
            }
        }
        #endregion

        #region Methods
        public void Draw()
        {
            //TODO: Delegate this update instead of performing it on every frame.
            mHeightMap = GameFileManager.TextureEditorRenderTarget2D;
            mHeightMap.GetData<Color>(colorDataBuffer);

            int iterator = 0;
            for (float xLoop = 0; xLoop < 256; xLoop++)
            {
                for (float yLoop = 0; yLoop < 256; yLoop++)
                {
                    //TODO: Add a sorting algorithm, or load a obj that happens to be the correct size.
                    mVextexData[iterator].Position.X = xLoop * 2;
                    mVextexData[iterator].Position.Z = yLoop * 2;
                    mVextexData[iterator].TextureCoordinate.X = (xLoop + 1.0f) / 256.0f;
                    mVextexData[iterator].TextureCoordinate.Y = (yLoop + 1.0f) / 256.0f;
                    mVextexData[iterator].Position.Y = -colorDataBuffer[iterator].R;

                    mVextexData[iterator].Normal.X = 0.0f;
                    mVextexData[iterator].Normal.Y = 1.0f;
                    mVextexData[iterator].Normal.Z = 0.0f;

                    iterator++;
                }
            }

            GameFileManager.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFileManager.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFileManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (EffectPass pass in GameFileManager.Effect.CurrentTechnique.Passes)
            {
                GameFileManager.Effect.CurrentTechnique = GameFileManager.Effect.Techniques["Standard"];
                GameFileManager.Effect.Parameters["xWorldViewProjectionMatrix"].SetValue(Matrix.CreateScale(3) * Matrix.Identity * GameFileManager.ViewMatrix * GameFileManager.ProjectionMatrix);
                GameFileManager.Effect.Parameters["xColorMap"].SetValue(mHeightMap);

                pass.Apply();

                GameFileManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mVextexData, 0, mVextexData.Length /2);
            }
        }
        #endregion
    }
}