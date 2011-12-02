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

            CollideWithTile();
        }
        #endregion

        #region Methods
        public override void Update()
        {
            mWorldMatrix = Matrix.CreateTranslation(mWorldPosition.X, mWorldPosition.Y, mWorldPosition.Z);

            base.Update();

            // DeleteTile(); // Uncomment this if you want to remove tiles. Leftclick to remove.
        }

        public override void Draw()
        {
            theFileManager.SpriteBatch.End();

            theFileManager.SpriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
            theFileManager.SpriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Model.Draw(ScaleMatrix * RotationMatrix * mWorldMatrix, theFileManager.ViewMatrix, theFileManager.ProjectionMatrix);
            //     theFileManager.SpriteBatch.End();

            theFileManager.SpriteBatch.Begin();
            theFileManager.SpriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            theFileManager.SpriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.None;
            theFileManager.SpriteBatch.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            theFileManager.SpriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            theFileManager.SpriteBatch.End();


            theFileManager.SpriteBatch.Begin();

            Vector3 projectedPosition = theFileManager.GraphicsDevice.Viewport.Project(WorldPosition, theFileManager.ProjectionMatrix, theFileManager.ViewMatrix, Matrix.Identity);
            Vector2 screenPosition = new Vector2(projectedPosition.X + 40, projectedPosition.Y - 150);

            Vector2 heightSpace = new Vector2(0, 20);

            bool isSelected = false;

            for(int loop = 0; loop < theFileManager.GizmoSelection.Count; loop++)
            {
                if (theFileManager.GizmoSelection[loop] == this)
                {
                    isSelected = true;
                }

            }

            if (isSelected)
            {
                theFileManager.SpriteBatch.DrawString(theFileManager.SpriteFont, "Position: " + mWorldPosition.ToString(), screenPosition, Color.White);
                theFileManager.SpriteBatch.DrawString(theFileManager.SpriteFont, "Rotation: " + mRotation.ToString(), screenPosition + heightSpace, Color.White);
                theFileManager.SpriteBatch.DrawString(theFileManager.SpriteFont, "Scale: " + mScale.ToString(), screenPosition + heightSpace * 2, Color.White);
                theFileManager.SpriteBatch.DrawString(theFileManager.SpriteFont, "Color: " + mColor.ToString(), screenPosition + heightSpace * 3, Color.White);
            }
        }

        protected void CollideWithTile()
        {
            for (int loop = 0; loop < theTileManager.List.Count; loop++)
            {
                if (theTileManager.List[loop] == this) continue;

                if (ScreenPosition == theTileManager.List[loop].ScreenPosition)
                {
                    theTileManager.Remove(theTileManager.List[loop]);
                }
            }
        }

        private void DeleteTile()
        {
            if (theInputManager.mouseLeftDrag)
            {
                if (CollisionRectangle.X - Graphic.Width / 2 < theInputManager.mousePosition.X &&
                    CollisionRectangle.X + Graphic.Width - Graphic.Width / 2 > theInputManager.mousePosition.X &&
                    CollisionRectangle.Y - Graphic.Height / 2 < theInputManager.mousePosition.Y &&
                    CollisionRectangle.Y + Graphic.Height - Graphic.Height / 2 > theInputManager.mousePosition.Y)
                {
                    theTileManager.Remove(this);
                }
            }
        }

        public bool Collision(Rectangle aCollisionRectangle)
        {
            if (CollisionRectangle.Intersects(aCollisionRectangle))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}