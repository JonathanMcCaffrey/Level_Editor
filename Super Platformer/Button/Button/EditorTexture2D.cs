using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public struct EditorTexture2D
    {
        #region Data
        public Texture2D mTexture2D;
        public Vector2 mPosition;
        public Rectangle mSourceRectangle;
        public Color mColor;
        public float mScale;
        public float mRotation;
        public SpriteEffects mSpriteEffect;
        public float mLayerDepth;
        public Vector2 mOrigin;
        #endregion

        #region Construction
        public EditorTexture2D(Texture2D aTexture2D, Vector2 aPosition, Color aColor, float aScale = 1.0f, float aRotation = 0.0f, SpriteEffects aSpriteEffects = SpriteEffects.None, float aLayerDepth = 0.0f)
        {
            mTexture2D = aTexture2D;
            mPosition = aPosition;
            mColor = aColor;
            mScale = aScale;
            mRotation = aRotation;
            mSpriteEffect = aSpriteEffects;
            mLayerDepth = aLayerDepth;
            mSourceRectangle = new Rectangle(0,0, mTexture2D.Width, mTexture2D.Height);
            mOrigin = new Vector2(mTexture2D.Width / 2, mTexture2D.Height / 2);
        }
        #endregion
    }
}
