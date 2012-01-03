using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LevelEditor
{
    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Data
        private AbstractGameScreen mWorldScreen;
        public AbstractGameScreen WorldScreen
        {
            get { return mWorldScreen; }
            set { mWorldScreen = value; }
        }
        #endregion

        #region Construction
        private ScreenManager(Game aGame)
            : base(aGame) { }
        static ScreenManager ScreenManagerInstance;
        static public ScreenManager Get(Game aGame)
        {
            if (null == ScreenManagerInstance)
            {
                ScreenManagerInstance = new ScreenManager(aGame);
            }

            return ScreenManagerInstance;
        }
        static public ScreenManager Get()
        {
            return ScreenManagerInstance;
        }
        #endregion

        #region Loading
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        #endregion

        #region GameLoop
        public override void Update(GameTime aGameTime)
        {
            if (mWorldScreen != null)
            {
                mWorldScreen.Update(aGameTime);
            }
        }

        public override void Draw(GameTime aGameTime)
        {
            if (mWorldScreen != null)
            {
                mWorldScreen.Draw(aGameTime);
            }
        }
        #endregion
    }
}
