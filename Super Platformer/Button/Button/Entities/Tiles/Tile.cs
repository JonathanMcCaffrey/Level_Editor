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
        private Model mModel = null;
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
        }
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
        

        private Texture2D mColorMap;
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
            if (FileManager.Get().TextureEditorRenderTarget2D != null)
            {
                Matrix[] tempTransforms = new Matrix[Model.Bones.Count];
                Model.CopyAbsoluteBoneTransformsTo(tempTransforms);


                for (int outerLoop = 0; outerLoop < Model.Meshes.Count; outerLoop++)
                {
                    ModelMesh tempMesh = Model.Meshes[outerLoop];

                    for (int innerLoop = 0; innerLoop < tempMesh.Effects.Count; innerLoop++)
                    {
                        BasicEffect tempEffect = tempMesh.Effects[innerLoop] as BasicEffect;

                        tempEffect.EnableDefaultLighting();
                        tempEffect.World = tempTransforms[tempMesh.ParentBone.Index] * ScaleMatrix * RotationMatrix * mWorldMatrix;
                        tempEffect.View = theFileManager.ViewMatrix;
                        tempEffect.Projection = theFileManager.ProjectionMatrix;

                        tempEffect.Texture = FileManager.Get().TextureEditorRenderTarget2D;
                    }

                    tempMesh.Draw();
                }
            }
        }
        #endregion
    }
}