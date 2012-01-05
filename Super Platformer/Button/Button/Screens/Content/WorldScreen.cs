﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace LevelEditor
{
    public class WorldScreen : AbstractGameScreen
    {
        #region Data
        EntityComponetManager theEntityComponetManager = EntityComponetManager.Get();
        Texture2D mBackgroundTexture;
        QuakeCamera mQuakeCamera;

        #endregion

        #region Construction
        public WorldScreen()
        {
            mBackgroundTexture = GameFileManager.LoadTexture2D("Background");

            mQuakeCamera = new QuakeCamera(GameFileManager.GraphicsDevice.Viewport);
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

            mQuakeCamera.Update();

            theEntityComponetManager.Update(aGameTime);
        }

        public override void Draw(GameTime aGameTime)
        {
            theEntityComponetManager.Draw(aGameTime); 
        }
        #endregion
    }
}
