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

namespace Button
{
    //<summary>
    // Custom Obj Loading for dynamic asset loading at runtime,
    // to allows users to drag and drop new assets into the editor
    // in realtime. Only used for static assets.
    //</summary>
    public class ObjModel
    {
        #region Singletons
        FileManager theFileManager;
        #endregion

        #region Fields
        private Matrix mIdenityMatrix = Matrix.Identity;
       
        private Matrix mProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 10, 100000);

        private Matrix mWorldMatrix = Matrix.Identity;
        private Vector3 mDimension;

        private VertexBuffer mVertexBuffer;
        private IndexBuffer mIndexBuffer;

        private Vector3[] mColorVerts;
        private uint mColorVertsCount = 0;
        private Vector3[] mNormalVerts;
        private uint mNormalVertsCount = 0;
        private Vector2[] mTextureCoordinates;
        private uint mTextureCoordinatesCount = 0;

        private uint[] mFaceList;
        private uint mFaceCount = 0;

        private Vector3 mHighestPointOnModel = Vector3.Zero;
        private Vector3 mLowestPointOnModel = Vector3.Zero;
        
        private VertexPositionColor[] mCollisionBox;
        private VertexPositionNormalTexture[] mModel;

        private StreamReader mStreamReader;
        private string rawData = "";

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

        public Vector3 HighestPointOnModel
        {
            get { return mHighestPointOnModel; }
        }
        public Vector3 LowestPontOnModel
        {
            get { return mLowestPointOnModel; }
        }
        #endregion

        #region Construction
        public ObjModel(string aObjFilePath)
        {
            // TODO: Change technique of extracting the current graphic device.
            theFileManager = FileManager.Get();

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
            mFaceList = new uint[mFaceCount * 9];

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

                    mFaceList[mFaceCount] = (uint)Convert.ToDouble(pileOne[0]) - 1;
                    mFaceList[mFaceCount + 1] = (uint)Convert.ToDouble(pileOne[1]) - 1;
                    mFaceList[mFaceCount + 2] = (uint)Convert.ToDouble(pileOne[2]) - 1;

                    mFaceList[mFaceCount + 3] = (uint)Convert.ToDouble(pileTwo[0]) - 1;
                    mFaceList[mFaceCount + 4] = (uint)Convert.ToDouble(pileTwo[1]) - 1;
                    mFaceList[mFaceCount + 5] = (uint)Convert.ToDouble(pileTwo[2]) - 1;

                    mFaceList[mFaceCount + 6] = (uint)Convert.ToDouble(pileThree[0]) - 1;
                    mFaceList[mFaceCount + 7] = (uint)Convert.ToDouble(pileThree[1]) - 1;
                    mFaceList[mFaceCount + 8] = (uint)Convert.ToDouble(pileThree[2]) - 1;

                    mFaceCount += 9;
                }
            }

            mStreamReader.Close();

            for (uint loop = 0; loop < mColorVerts.Length; loop++)
            {
                if (mColorVerts[loop].X > mHighestPointOnModel.X)
                {
                    mHighestPointOnModel.X = mColorVerts[loop].X;
                }
                if (mColorVerts[loop].Y > mHighestPointOnModel.Y)
                {
                    mHighestPointOnModel.Y = mColorVerts[loop].Y;
                }
                if (mColorVerts[loop].Z > mHighestPointOnModel.Z)
                {
                    mHighestPointOnModel.Z = mColorVerts[loop].Z;
                }

                if (mColorVerts[loop].X < mLowestPointOnModel.X)
                {
                    mLowestPointOnModel.X = mColorVerts[loop].X;
                }
                if (mColorVerts[loop].Y < mLowestPointOnModel.Y)
                {
                    mLowestPointOnModel.Y = mColorVerts[loop].Y;
                }
                if (mColorVerts[loop].Z < mLowestPointOnModel.Z)
                {
                    mLowestPointOnModel.Z = mColorVerts[loop].Z;
                }
            }

            mDimension = new Vector3(mHighestPointOnModel.X - mLowestPointOnModel.X, mHighestPointOnModel.Y - mLowestPointOnModel.Y, mHighestPointOnModel.Z - mLowestPointOnModel.Z);

            mCollisionBox = new VertexPositionColor[30];

            Color temporaryColor = Color.Red;
            int temporaryInt = 0;

            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;

            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;


            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;

            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;


            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mLowestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;

            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mHighestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mHighestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;
            mCollisionBox[temporaryInt] = new VertexPositionColor(new Vector3(mLowestPointOnModel.X, mHighestPointOnModel.Y, mLowestPointOnModel.Z), temporaryColor); temporaryInt++;

            mModel = new VertexPositionNormalTexture[mFaceList.Length / 3];

            int index = 0;
            for (int loop = 0; loop < mModel.Length; loop++)
            {
                mModel[loop] = new VertexPositionNormalTexture(mColorVerts[mFaceList[index]], mNormalVerts[mFaceList[index + 2]], mTextureCoordinates[mFaceList[index + 1]]);
                index += 3;
            }

            mVertexBuffer = new VertexBuffer(theFileManager.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, mModel.Length, BufferUsage.WriteOnly);
            mVertexBuffer.SetData(mModel);
            mIndexBuffer = new IndexBuffer(theFileManager.GraphicsDevice, typeof(int), mFaceList.Length, BufferUsage.None);
            mIndexBuffer.SetData(mFaceList);
        }
        #endregion

        #region Methods

        #region ToString
        public override string ToString()
        {
            return "ObjModel.cs";
        }
        #endregion
        #endregion
    }
}
