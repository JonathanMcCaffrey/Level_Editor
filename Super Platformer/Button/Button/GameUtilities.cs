using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    // <summary>
    // Contains the random storage of generic game helper methods. I am currently not using it,
    // and have no plans on transfering it to the main engine.
    // </summary>
    public static class GameUtilities
    {
        #region Methods
        public static Rectangle GetRectangleFromTexture2D(Texture2D aTexture2D)
        {
            Rectangle temporaryRectangle = new Rectangle(0, 0, (int)aTexture2D.Width, (int)aTexture2D.Height);

            return temporaryRectangle;
        }

        public static Rectangle GetRectangleFromGraphicsDevice(GraphicsDevice aGraphicsDevice)
        {
            Rectangle temporaryRectangle = new Rectangle(0, 0, (int)aGraphicsDevice.Viewport.Width, (int)aGraphicsDevice.Viewport.Height);

            return temporaryRectangle;
        }

        public static Rectangle SkimRectangle(Rectangle aRectangle, int aAmountToSkim)
        {
            Rectangle temporaryRectangle = aRectangle;
            temporaryRectangle.X += aAmountToSkim;
            temporaryRectangle.Y += aAmountToSkim;
            temporaryRectangle.Width -= aAmountToSkim;
            temporaryRectangle.Height -= aAmountToSkim;

            return temporaryRectangle;
        }

        public static Vector2 GetOriginFromTexture(Texture2D aTexture2D)
        {
            Vector2 temporaryOrigin = new Vector2(aTexture2D.Width / 2, aTexture2D.Height / 2);

            return temporaryOrigin;
        }

        public static Vector2 GetOriginFromRectangle(Rectangle aRectangle)
        {
            Vector2 temporaryOrigin = new Vector2(aRectangle.Width / 2, aRectangle.Height / 2);

            return temporaryOrigin;
        }

        public static Vector2 GetViewportFromGraphicsDevice(GraphicsDevice aGraphicsDevice)
        {
            Vector2 temporaryViewport = new Vector2(aGraphicsDevice.Viewport.Width, aGraphicsDevice.Viewport.Height);

            return temporaryViewport;
        }

        public static Vector2 GetScreenCenterFromGraphicsDevice(GraphicsDevice aGraphicsDevice)
        {
            Vector2 temporaryScreenmCenter = new Vector2(aGraphicsDevice.Viewport.Width / 2, aGraphicsDevice.Viewport.Height / 2);

            return temporaryScreenmCenter;
        }
        
        public static Rectangle GetCollisionBox(Vector3 aLowest, Vector3 aHeighest, Vector3 aTranslation, Vector3 aTile)
        {
            Rectangle temporaryRectangle = new Rectangle(
                                                        (int)aLowest.X + (int)aTranslation.X,
                                                        (int)aLowest.Z + (int)aTranslation.Z,
                                                        ((int)aHeighest.X - (int)aLowest.X) * (int)aTile.X,
                                                        ((int)aHeighest.Z - (int)aLowest.Z) * (int)aTile.Z);

            return temporaryRectangle;
        }

        public static BoundingBox GetBoundingBox(Vector3 aLowest, Vector3 aHighest, Vector3 aTranslation, Vector3 aTile)
        {
            BoundingBox temporaryBox = new BoundingBox(aLowest * aTile + aTranslation, aHighest * aTile + aTranslation);

            return temporaryBox;
        }

        public static BoundingSphere GetBoundingSphere(Vector3 aLowest, Vector3 aHighest, Vector3 aTranslation, Vector3 aTile)
        {
            Vector3 temporaryRadius = new Vector3((aHighest.X - aLowest.X) / 2, (aHighest.Y - aLowest.Y) / 2, (aHighest.Z - aLowest.Z) / 2) * aTile;
            BoundingSphere temporarySphere = new BoundingSphere(aTranslation, (temporaryRadius.X + temporaryRadius.Y + temporaryRadius.Z) / 3);

            return temporarySphere;
        }

        public static float GetFloatFromColor(Color aColor)
        {
            return ((aColor.R + aColor.B + aColor.G) / (3.0f * 255.0f)) * (aColor.A / 255.0f);
        }
        #endregion
    }
}