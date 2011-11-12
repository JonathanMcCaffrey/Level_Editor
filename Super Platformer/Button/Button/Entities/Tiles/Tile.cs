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
        Model mModel = FileManager.Get().LoadModel("Cube");
        Matrix mWorldMatrix = Matrix.Identity;
        Matrix mProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 1, 10000);
        Matrix mViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 500), Vector3.Zero, Vector3.Up);


        #region Construction
        public Tile()
        {
            Initialize();
        }

        public Tile(Vector2 aCoordinate)
        {
            mWorldPosition = aCoordinate;

            theTileManager.Add(this);

            Initialize();
        }

        private void Initialize()
        {
            mManager = theTileManager;
            mName = "tile";

            CollideWithTile();
        }
        #endregion

        #region Methods
        public override void Update()
        {
            mWorldMatrix = Matrix.CreateTranslation(mWorldPosition.X, -mWorldPosition.Y, 0);

            base.Update();

           // DeleteTile(); // Uncomment this if you want to remove tiles. Leftclick to remove.
        }

        public override void Draw()
        {

            theFileManager.SpriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
            theFileManager.SpriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            mModel.Draw(mWorldMatrix, mViewMatrix, mProjectionMatrix);
            theFileManager.SpriteBatch.End();

            theFileManager.SpriteBatch.Begin();
            theFileManager.SpriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            theFileManager.SpriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.None;
            theFileManager.SpriteBatch.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            theFileManager.SpriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

            if (IsOnScreen)
            {
                theFileManager.SpriteBatch.Draw(Graphic, ScreenPosition, SourceRectangle, Color, Rotation, Origin, Scale, SpriteEffects, LayerDepth);
            }
            theFileManager.SpriteBatch.End();

            theFileManager.SpriteBatch.Begin();
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
                if (CollisionRectangle.X - Graphic.Width/2 < theInputManager.mousePosition.X &&
                    CollisionRectangle.X + Graphic.Width - Graphic.Width / 2 > theInputManager.mousePosition.X &&
                    CollisionRectangle.Y - Graphic.Height  / 2< theInputManager.mousePosition.Y &&
                    CollisionRectangle.Y + Graphic.Height - Graphic.Height /2> theInputManager.mousePosition.Y)
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