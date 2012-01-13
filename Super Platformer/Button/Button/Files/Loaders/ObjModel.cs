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
    // to allows users to drag and drop new assets shorto the editor
    // in realtime. Only used for static assets.
    //</summary>
    public class ObjModel
    {
        #region Fields
        private Matrix mIdenityMatrix = Matrix.Identity;
       
        private Matrix mProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 10, 100000);

        private Matrix mWorldMatrix = Matrix.Identity;
        private Vector3 mDimension;

        private VertexBuffer mVertexBuffer;
        private IndexBuffer mIndexBuffer;

        private Vector3[] mColorVerts;
        private ushort mColorVertsCount = 0;
        private Vector3[] mNormalVerts;
        private ushort mNormalVertsCount = 0;
        private Vector2[] mTextureCoordinates;
        private ushort mTextureCoordinatesCount = 0;

        private short[] mFaceList;
        private short mFaceCount = 0;

        private Vector3 mHighestPoshortOnModel = Vector3.Zero;
        private Vector3 mLowestPoshortOnModel = Vector3.Zero;
        
        private VertexPositionColor[] mCollisionBox;
        private VertexPositionNormalTexture[] mObjModel;

        private StreamReader mStreamReader;
        private string rawData = "";

        private Effect mEffect;

        #endregion

        #region Properties
        private Matrix ViewMatrix
        {
            get { return Matrix.CreateLookAt(new Vector3(1, 50, 0), Vector3.Zero, Vector3.Up); }
        }
        public Vector3 Dimension
        {
            get { return mDimension; }
        }

        public Vector3 HighestPoshortOnModel
        {
            get { return mHighestPoshortOnModel; }
        }
        public Vector3 LowestPontOnModel
        {
            get { return mLowestPoshortOnModel; }
        }
        #endregion

        #region Construction
        public ObjModel(string aObjFilePath)
        {
            // TODO: Change technique of extracting the current graphic device.
            mEffect = GameFiles.Effect;

            mStreamReader = new StreamReader(aObjFilePath);

            while (!mStreamReader.EndOfStream)
            {
                rawData = mStreamReader.ReadLine();

                if (rawData.StartsWith("v "))
                {
                    mColorVertsCount++;
                }
                else if (rawData.StartsWith("vt "))
                {
                    mTextureCoordinatesCount++;
                }
                else if (rawData.StartsWith("vn "))
                {
                    mNormalVertsCount++;
                }
                else if (rawData.StartsWith("f "))
                {
                    mFaceCount++;
                }
            }

            mStreamReader.Close();

            mColorVerts = new Vector3[mColorVertsCount];
            mNormalVerts = new Vector3[mNormalVertsCount];
            mTextureCoordinates = new Vector2[mTextureCoordinatesCount];
            mFaceList = new short[mFaceCount * 9];

            mColorVertsCount = 0;
            mNormalVertsCount = 0;
            mTextureCoordinatesCount = 0;
            mFaceCount = 0;

            mStreamReader = new StreamReader(aObjFilePath);

            while (!mStreamReader.EndOfStream)
            {
                rawData = mStreamReader.ReadLine();

                if (rawData.StartsWith("v "))
                {
                    string[] organizedData = rawData.Split(' ');

                    mColorVerts[mColorVertsCount].X = (float)Convert.ToDecimal(organizedData[2]);
                    mColorVerts[mColorVertsCount].Y = (float)Convert.ToDecimal(organizedData[3]);
                    mColorVerts[mColorVertsCount].Z = (float)Convert.ToDecimal(organizedData[4]);

                    mColorVertsCount++;
                }
                else if (rawData.StartsWith("vt "))
                {
                    string[] organizedData = rawData.Split(' ');

                    mTextureCoordinates[mTextureCoordinatesCount].X = (float)Convert.ToDouble(organizedData[1]);
                    mTextureCoordinates[mTextureCoordinatesCount].Y = (float)Convert.ToDouble(organizedData[2]);

                    mTextureCoordinatesCount++;
                }
                else if (rawData.StartsWith("vn "))
                {
                    string[] organizedData = rawData.Split(' ');

                    mNormalVerts[mNormalVertsCount].X = (float)Convert.ToDouble(organizedData[1]);
                    mNormalVerts[mNormalVertsCount].Y = (float)Convert.ToDouble(organizedData[2]);
                    mNormalVerts[mNormalVertsCount].Z = (float)Convert.ToDouble(organizedData[3]);

                    mNormalVertsCount++;
                }
            }

            mStreamReader.Close();

            mStreamReader = new StreamReader(aObjFilePath);

            while (!mStreamReader.EndOfStream)
            {
                rawData = mStreamReader.ReadLine();

                if (rawData.StartsWith("f "))
                {
                    string[] organizedData = rawData.Split(' ');

                    string[] pileOne = organizedData[1].Split('/');
                    string[] pileTwo = organizedData[2].Split('/');
                    string[] pileThree = organizedData[3].Split('/');

                    mFaceList[mFaceCount] = (short)(Convert.ToDouble(pileOne[0]) - 1);
                    mFaceList[mFaceCount + 1] = (short)(Convert.ToDouble(pileOne[1]) - 1);
                    mFaceList[mFaceCount + 2] = (short)(Convert.ToDouble(pileOne[2]) - 1);

                    mFaceList[mFaceCount + 3] = (short)(Convert.ToDouble(pileTwo[0]) - 1);
                    mFaceList[mFaceCount + 4] = (short)(Convert.ToDouble(pileTwo[1]) - 1);
                    mFaceList[mFaceCount + 5] = (short)(Convert.ToDouble(pileTwo[2]) - 1);

                    mFaceList[mFaceCount + 6] = (short)(Convert.ToDouble(pileThree[0]) - 1);
                    mFaceList[mFaceCount + 7] = (short)(Convert.ToDouble(pileThree[1]) - 1);
                    mFaceList[mFaceCount + 8] = (short)(Convert.ToDouble(pileThree[2]) - 1);

                    mFaceCount += 9;
                }
            }

            mStreamReader.Close();

            for (ushort loop = 0; loop < mColorVerts.Length; loop++)
            {
                if (mColorVerts[loop].X > mHighestPoshortOnModel.X)
                {
                    mHighestPoshortOnModel.X = mColorVerts[loop].X;
                }
                if (mColorVerts[loop].Y > mHighestPoshortOnModel.Y)
                {
                    mHighestPoshortOnModel.Y = mColorVerts[loop].Y;
                }
                if (mColorVerts[loop].Z > mHighestPoshortOnModel.Z)
                {
                    mHighestPoshortOnModel.Z = mColorVerts[loop].Z;
                }

                if (mColorVerts[loop].X < mLowestPoshortOnModel.X)
                {
                    mLowestPoshortOnModel.X = mColorVerts[loop].X;
                }
                if (mColorVerts[loop].Y < mLowestPoshortOnModel.Y)
                {
                    mLowestPoshortOnModel.Y = mColorVerts[loop].Y;
                }
                if (mColorVerts[loop].Z < mLowestPoshortOnModel.Z)
                {
                    mLowestPoshortOnModel.Z = mColorVerts[loop].Z;
                }
            }

            mDimension = new Vector3(mHighestPoshortOnModel.X - mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y - mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z - mLowestPoshortOnModel.Z);

            mCollisionBox = new VertexPositionColor[30];

            Color temporaryColor = Color.Red;
            short temporaryshort = 0;

            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;

            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;


            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;

            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;


            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mLowestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;

            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mHighestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mHighestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
            mCollisionBox[temporaryshort] = new VertexPositionColor(new Vector3(mLowestPoshortOnModel.X, mHighestPoshortOnModel.Y, mLowestPoshortOnModel.Z), temporaryColor); temporaryshort++;
             
            mObjModel = new VertexPositionNormalTexture[mFaceList.Length / 3];

            short index = 0;
            for (short loop = 0; loop < mObjModel.Length; loop++)
            {
                mObjModel[loop] = new VertexPositionNormalTexture(mColorVerts[mFaceList[index]], mNormalVerts[mFaceList[index + 2]], mTextureCoordinates[mFaceList[index + 1]]);
                index += 3;
            }

            mVertexBuffer = new VertexBuffer(GameFiles.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, mObjModel.Length, BufferUsage.WriteOnly);
            mVertexBuffer.SetData(mObjModel);
            mIndexBuffer = new IndexBuffer(GameFiles.GraphicsDevice, typeof(short), mFaceList.Length, BufferUsage.None);
            mIndexBuffer.SetData(mFaceList);
        }
        #endregion

        #region Methods
        public void Draw(Tile aTile)
        {
            GameFiles.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
            GameFiles.GraphicsDevice.BlendState = BlendState.Opaque;
            GameFiles.GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            foreach (EffectPass pass in mEffect.CurrentTechnique.Passes)
            {
                mEffect.CurrentTechnique = mEffect.Techniques["Standard"];
                mEffect.Parameters["xWorldViewProjectionMatrix"].SetValue(aTile.ScaleMatrix * aTile.RotationMatrix * aTile.WorldMatrix * GameFiles.ViewMatrix * GameFiles.ProjectionMatrix);

                mEffect.Parameters["xColorMap"].SetValue(aTile.Graphic);

                pass.Apply();

                GameFiles.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, mObjModel, 0, mObjModel.Length / 3);
            }
        }

        #region ToString
        public override string ToString()
        {
            return "ObjModel.cs";
        }
        #endregion
        #endregion
    }
}
