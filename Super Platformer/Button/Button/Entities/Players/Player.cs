using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Button
{
    public class Player : AbstractEntity
    {
        #region Data
        float mHealthSize = 10;
        float mHealthLeft = 10;

        public override Vector3 WorldPosition
        {
            get
            {
                return mWorldPosition;
            }
        }

        private Vector3 mScreenPosition = Vector3.Zero;
        public override Vector3 ScreenPosition
        {
            get
            {
                float SKIM = 150;

                if (mScreenPosition.X - Origin.X < 0 + SKIM)
                {
                    mScreenPosition.X = 0 + Origin.X + SKIM;
                }
                if (mScreenPosition.X + Origin.X > theFileManager.GraphicsDevice.Viewport.Width - SKIM)
                {
                    mScreenPosition.X = theFileManager.GraphicsDevice.Viewport.Width - Origin.X - SKIM;
                }

                if (mScreenPosition.Y - Origin.Y < 0 + SKIM)
                {
                    mScreenPosition.Y = 0 + Origin.Y + SKIM;
                }
                if (mScreenPosition.Y + Origin.Y > theFileManager.GraphicsDevice.Viewport.Height - SKIM)
                {
                    mScreenPosition.Y = theFileManager.GraphicsDevice.Viewport.Height - Origin.Y - SKIM;
                }

                return mScreenPosition;
            }
        }

        public override bool IsOnScreen
		{
            get
            {
			    bool tempBoolean = false;
			
			    if (ScreenPosition.X > 0 && ScreenPosition.X < 1024 && 
				    ScreenPosition.Y > 0 && ScreenPosition.Y  < 1024)
			    {
				    tempBoolean = true;
			    }
			
			    return tempBoolean;
            }
		}

        private CollisionMachine mCollisionMachine;

        private float mSpeed = 4.0f;
        public float Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }

        public override Rectangle CollisionRectangle
        {
            get { return new Rectangle((int)ScreenPosition.X + 10, (int)ScreenPosition.Y - 5, Graphic.Width - 60, Graphic.Height - 55); }
        }
        #endregion

        #region Construction
        public Player()
        {
            Initialize();
        }

        private Player(string aFilePathToGraphic, Vector3 aScreenCoordinate, Vector3 aWorldCoordinate)
        {
            mScreenPosition = aScreenCoordinate;
            mWorldPosition = aWorldCoordinate;

           thePlayerManager.Add(this);

           Initialize();
        }

        private void Initialize()
        {
            mCollisionMachine = new EntityCollision(this);
            mManager = thePlayerManager;
            mFilePathToGraphic = "MainCharacter";
            Name = "player";
        }

        static public void CreatePlayer(string aFilePathToGraphic, Vector3 aScreenCoordinate, Vector3 aWorldCoordinate)
        {
            new Player(aFilePathToGraphic, aScreenCoordinate, aWorldCoordinate);
        }
        #endregion

        #region Methods
        public override void Update()
        {
            base.Update();

            Movement();
            Gun();
        }

        private void Movement()
        {
            Vector3 positionPostWorldCollision = WorldPosition;
            Vector3 positionPostScreenCollision = ScreenPosition;

            mWorldPosition = positionPostWorldCollision;

            bool isMoving = false;

            Velocity -= new Vector3(0, 0, -Speed / 20) * 10;

            if (theInputManager.MulitKeyPressInput(Keys.Up))
            {
                Velocity += new Vector3(0,0, -Speed / 20) * 20;
                mRotation = (float)Math.Atan2(Velocity.X, -Velocity.Y);
                isMoving = true;
            }
            if (theInputManager.MulitKeyPressInput(Keys.Right))
            {
                Velocity += new Vector3(Speed / 20, 0, 0);
                mRotation = (float)Math.Atan2(Velocity.X, -Velocity.Y);
                isMoving = true;
            }
            if (theInputManager.MulitKeyPressInput(Keys.Left))
            {
                Velocity += new Vector3(-Speed / 20, 0, 0);
                mRotation = (float)Math.Atan2(Velocity.X, -Velocity.Y);
                isMoving = true;
            }

            if (mVelocity.X > Speed) mVelocity.X = Speed;
            if (mVelocity.X < -Speed) mVelocity.X = -Speed;
            if (mVelocity.Y > Speed) mVelocity.Y = Speed;
            if (mVelocity.Y < -Speed) mVelocity.Y = -Speed;
            

            if (isMoving == false)
            {
                Velocity *= 0.85f;
            }
            Velocity.Normalize();


            mWorldPosition.X += Velocity.X;
            mScreenPosition.X += Velocity.X;
            if (CollideWithTile() || CollideWithPlayer())
            {
                mWorldPosition.X = positionPostWorldCollision.X;
                mScreenPosition.X = positionPostScreenCollision.X;
                mOldPosition = mWorldPosition;
            }

            mCollisionMachine.Update();

            mWorldPosition.Y += Velocity.Y;
            mScreenPosition.Y += Velocity.Y;
            if (CollideWithTile() || CollideWithPlayer())
            {
                mWorldPosition.Y = positionPostWorldCollision.Y;
                mScreenPosition.Y = positionPostScreenCollision.Y;
                mOldPosition = mWorldPosition;
            }

            mCollisionMachine.Update();
        }



        private bool CollideWithTile()
        {
            for (int loop = 0; loop < theTileManager.List.Count; loop++)
            {
                if (theTileManager.List[loop].IsCollidable == false) continue;

                if (CollisionRectangle.Intersects(theTileManager.List[loop].CollisionRectangle))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CollideWithPlayer()
        {
            for (int loop = 0; loop < thePlayerManager.List.Count; loop++)
            {
                if (thePlayerManager.List[loop] == this) continue;

                if (CollisionRectangle.Intersects(thePlayerManager.List[loop].CollisionRectangle))
                {
                    return true;
                }
            }
            return false;
        }

        void Gun()
        {
            Vector3 tempVector;
            tempVector = mScreenPosition - new Vector3(theInputManager.mousePosition.X, 0, theInputManager.mousePosition.Y);
            tempVector.Normalize();
            mGunDirection = (float)Math.Atan2(tempVector.Y, tempVector.X) - MathHelper.PiOver2;

            if (theInputManager.mouseLeftDrag)
            {
           //     TankShell.CreateProjectile(ScreenPosition, WorldPosition - new Vector3(0, 0, 25), this);
            }
        }


        public override void Draw()
        {
            if (IsOnScreen)
            {
             //   theFileManager.SpriteBatch.Draw(Graphic, ScreenPosition, SourceRectangle, Color, Rotation, Origin + new Vector2(0, 10), Scale, SpriteEffects, LayerDepth);
            }
        }

        public override void Damage()
        {
            mHealthLeft--;

            mColor = new Color(1.0f, mHealthLeft / mHealthSize, mHealthLeft / mHealthSize);
        }
        #endregion
    }
}
