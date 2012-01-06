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
        private Texture2D mHeightMap;
        private VertexPositionNormalTexture[] mVextexData = new VertexPositionNormalTexture[65536];
        private Color[] colorDataBuffer = new Color[65536];
        #endregion

        #region Construction
        public Terrain()
        {
            mHeightMap = GameFiles.LoadTexture2D("TextureEditorTest");

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
            mHeightMap = GameFiles.TextureEditorRenderTarget2D;
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

            GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (EffectPass pass in GameFiles.Effect.CurrentTechnique.Passes)
            {
                GameFiles.Effect.CurrentTechnique = GameFiles.Effect.Techniques["Standard"];
                GameFiles.Effect.Parameters["xWorldViewProjectionMatrix"].SetValue(Matrix.CreateScale(3) * Matrix.Identity * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);
                GameFiles.Effect.Parameters["xColorMap"].SetValue(mHeightMap);

                pass.Apply();

                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mVextexData, 0, mVextexData.Length /2);
            }
        }
        #endregion
    }
}