using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    // This is Deprecated.
    public class AbstractEntity
    {
        #region Singletons
        protected TileManager theTileManager = TileManager.Get();
        protected ScreenManager theScreenManager = ScreenManager.Get();
        #endregion

        #region Data
        protected AbstractEntityManager mManager;
        public AbstractEntityManager Manager
        {
            get { return mManager; }
        }

        private string mName;
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        const float BORDER_LENGTH = 4.0f;
        protected string mFilePathToGraphic = "IconOne";
        public Texture2D Graphic
        {
            get { return GameFiles.LoadTexture2D(mFilePathToGraphic); }// GameFileManager.LoadTexture2D(FilePathToGraphic); }
        }
        public string FilePathToGraphic
        {
            get { return mFilePathToGraphic; }
            set { mFilePathToGraphic = value; }
        }

        protected string mFilePathToModel = "Asteroid.obj";
        public string FilePathToModel
        {
            get { return mFilePathToModel; }
            set { mFilePathToModel = value; }
        }

        protected Vector3 mWorldPosition = Vector3.Zero;
        public virtual Vector3 WorldPosition
        {
            get
            {
                return mWorldPosition;
            }
            set
            {
                mWorldPosition = value;
            }
        }

        protected Vector3 mOldPosition = Vector3.Zero;
        public Vector3 OldPosition
        {
            get { return mOldPosition; }
            set { mOldPosition = value; }
        }

        protected Vector3 mVelocity = Vector3.Zero;
        public virtual Vector3 Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        public virtual Vector3 ScreenPosition
        {
            get
            {
                return WorldPosition;
            }
        }

        private Vector3 mForward = Vector3.Forward;
        public Vector3 Forward
        {
            get { return mForward; }
            set { mForward = value; }
        }

        private Vector3 mUp = Vector3.Up;
        public Vector3 Up
        {
            get { return mUp; }
            set { mUp = value; }
        }

        public virtual bool IsOnScreen
        {
            get
            {
                bool tempBoolean = false;

                if (ScreenPosition.X + Graphic.Width > 0 && ScreenPosition.X < 1024 - Graphic.Width &&
                    ScreenPosition.Y + Graphic.Height > 0 && ScreenPosition.Y < 1024 - Graphic.Height)
                {
                    tempBoolean = true;
                }

                return tempBoolean;
            }
        }

        protected float mGunDirection = 0;
        public float GunDirection
        {
            get { return mGunDirection; }
        }

        protected Rectangle SourceRectangle
        {
            get
            {
                Rectangle temporaryRectangle = new Rectangle(0, 0, (int)Graphic.Width, (int)Graphic.Height);

                return temporaryRectangle;
            }
        }

        protected bool isCollidable = true;
        public bool IsCollidable
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }

        float berth = 300;
        public virtual BoundingBox BoundingBox
        {
            get { return new BoundingBox(WorldPosition - new Vector3(berth, berth, berth), WorldPosition + new Vector3(berth, berth, berth)); }
        }

        public virtual Rectangle CollisionRectangle
        {
            get { return new Rectangle((int)ScreenPosition.X, (int)ScreenPosition.Y, Graphic.Width, Graphic.Height); }
        }

        protected Color mColor = Color.White;
        public Color Color
        {
            get { return mColor; }
            set { mColor = value; }
        }

        public Vector3 mRotation = Vector3.Zero;
        public Vector3 Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }

        protected Vector2 Origin
        {
            get
            {
                Vector2 temporaryOrigin = new Vector2(Graphic.Width / 2, Graphic.Height / 2);

                return temporaryOrigin;
            }
        }

        protected Vector3 mScale = Vector3.One;
        public Vector3 Scale
        {
            get { return mScale; }
            set { mScale = value; }
        }

        protected SpriteEffects mSpriteEffects = SpriteEffects.None;
        public SpriteEffects SpriteEffects
        {
            get { return mSpriteEffects; }
            set { mSpriteEffects = value; }
        }

        protected float mLayerDepth = 0;
        public float LayerDepth
        {
            get { return mLayerDepth; }
            set { mLayerDepth = value; }
        }

        protected bool IsBorderSelected
        {
            get
            {
                /*       if (theInputManager.mousePosition.X > mWorldPosition.X - Origin.X &&
                           theInputManager.mousePosition.X < mWorldPosition.X + Origin.X)
                       {
                           if (theInputManager.mousePosition.Y > mWorldPosition.Y - Origin.Y &&
                               theInputManager.mousePosition.Y < mWorldPosition.Y + Origin.Y)
                           {
                               if (IsSelected) return false;

                               Color = Color.Gray;

                               return true;
                           }
                       }
                       */
                Color = Color.Wheat;

                return false;
            }
        }

        protected bool IsSelected
        {
            get
            {
                /*   if (theInputManager.mousePosition.X > mWorldPosition.X - Origin.X + BORDER_LENGTH &&
                       theInputManager.mousePosition.X < mWorldPosition.X + Origin.X - BORDER_LENGTH)
                   {
                       if (theInputManager.mousePosition.Y > mWorldPosition.Y - Origin.Y + BORDER_LENGTH &&
                           theInputManager.mousePosition.Y < mWorldPosition.Y + Origin.Y - BORDER_LENGTH)
                       {
                           Color = Color.White;

                           return true;
                       }
                   }*/

                return false;
            }
        }
        #endregion

        #region Construction
        public AbstractEntity()
        {
        }

        protected AbstractEntity(Vector3 aCoordinate)
        {
            mWorldPosition = aCoordinate;

            mManager.Add(this);
        }

        static public void CreateEntity(Vector3 aCoordinate)
        {
            new AbstractEntity(aCoordinate);
        }
        #endregion

        #region Methods
        public virtual void Update()
        {
            mOldPosition = mWorldPosition;
        }

        public virtual void Draw() { }

        public virtual void Create(Vector3 aCoordinate) { }
        #endregion
    }
}