using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Button
{
    public class WorldScreen : AbstractGameScreen
    {
        #region Data
        EntityComponetManager theEntityComponetManager = EntityComponetManager.Get();

        Texture2D mBackgroundTexture;
        #endregion

        #region Construction
        public WorldScreen()
        {
            mBackgroundTexture = theFileManager.LoadTexture2D("Background");
            //   Enemy.CreateEnemy(Vector2.Zero);
        }

        public WorldScreen(string aFilePath)
        {
            theTileManager.Clear();
            theTileManager.Load(aFilePath);
        }

        #endregion

        #region Methods
        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);

            theCollisionManager.Reset();

            theEntityComponetManager.Update(aGameTime);

            theButtonManager.Update(aGameTime);
            theProjectileManager.Update(aGameTime);
        }

        public override void Draw(GameTime aGameTime)
        {
            SpriteBatch.Draw(mBackgroundTexture, new Rectangle(-200, 0, 1000, 800), Color.White);

            theEntityComponetManager.Draw(aGameTime);

            theButtonManager.Draw(aGameTime);
            theProjectileManager.Draw(aGameTime);
        }
        #endregion
    }
}
