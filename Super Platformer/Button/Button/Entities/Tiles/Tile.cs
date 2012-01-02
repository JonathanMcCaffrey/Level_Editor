using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Button
{
    public class Tile : AbstractEntity
    {
        #region Data
   
        /*private Model mModel = null;
        public Model Model
        {
            get
            {
                if (mModel == null)
                {
                    mModel = FileManager.Get().LoadModel(FilePathToModel);
                }

                return mModel;
            }
            set { mModel = value; }
        }*/

        private string mObjFilePath;
        private ObjModel mObjModel;

        Matrix mWorldMatrix = Matrix.Identity;
        public Matrix WorldMatrix
        {
            get { return Matrix.CreateTranslation(mWorldPosition.X, mWorldPosition.Y, mWorldPosition.Z); }
            set { mWorldMatrix = value; }
        }

        Matrix mScaleMatrix;
        public Matrix ScaleMatrix
        {
            get { return Matrix.CreateScale(mScale.X, mScale.Y, mScale.Z); }
            set { mScaleMatrix = value; }
        }

        Matrix mRotationMatrix;
        public Matrix RotationMatrix
        {
            get { return Matrix.CreateRotationX(mRotation.X) * Matrix.CreateRotationY(mRotation.Y) * Matrix.CreateRotationZ(mRotation.Z); }
            set { mRotationMatrix = value; }
        }

        private RenderTarget2D mRenderTarget;
        public RenderTarget2D RenderTarget
        {
            get { return mRenderTarget; }
            set { mRenderTarget = value; }
        }
        

        private Texture2D mColorMap = null;
        public Texture2D ColorMap
        {
            get { return mColorMap; }
            set { mColorMap = value; }
        }
        #endregion

        #region Construction
        public Tile()
        {
            Initialize();
        }

        public Tile(Vector3 aCoordinate)
        {
            mWorldPosition = aCoordinate;

            theTileManager.Add(this);

            Initialize();
        }

        private void Initialize()
        {
            mManager = theTileManager;
            Name = "tile";
            mRenderTarget = new RenderTarget2D(FileManager.Get().GraphicsDevice, 512, 512);

            mObjFilePath = "C:\\Users\\mcca0442\\Desktop\\trunk\\Super Platformer\\Button\\ButtonContent\\Assets\\Asteroid.obj";
            mObjModel = new ObjModel(mObjFilePath);
            ColorMap = theFileManager.LoadTexture2D("Necron");
        }
        #endregion

        #region Methods
        public override void Update()
        {
            mWorldMatrix = Matrix.CreateTranslation(mWorldPosition.X, mWorldPosition.Y, mWorldPosition.Z);

            if (!once)
            {
                FileManager.Get().GraphicsDevice.SetRenderTarget(mRenderTarget);
                FileManager.Get().SpriteBatch.Begin();

                FileManager.Get().SpriteBatch.Draw(mColorMap, Vector2.Zero, Color.White);

                FileManager.Get().SpriteBatch.End();
                FileManager.Get().GraphicsDevice.SetRenderTarget(null);
            }
        }

        bool once = false;
        public override void Draw()
        {
            mObjModel.Draw(this);
        }

        public void Clone()
        {
            try
            {
                Tile clonedTile = new Tile();
                clonedTile.WorldPosition = this.WorldPosition;
                clonedTile.FilePathToGraphic = this.FilePathToGraphic;
                clonedTile.FilePathToModel = this.FilePathToModel;
                clonedTile.mObjModel = new ObjModel(this.FilePathToModel);

                theTileManager.Add(clonedTile);
            }
            catch
            {
                Console.WriteLine("{0} has an incorrect filepath of {1}. {2}.", "clonedTile", this.FilePathToModel, this.ToString());

            }
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "EditorAssetLoader.cs";
        }
        #endregion
        #endregion
    }
}