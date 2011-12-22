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

#region Program
namespace Button
{
#if WINDOWS || XBOX

    static class TheGame
    {
        [STAThread]
        static int Main()
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }

            return 0;
        }
    }
#endif
}
#endregion

namespace Button
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Singletons
        protected FileManager theFileManager;
        protected InputManager theInputManager;
        protected UtilityManager theUtilityManager;
        protected TileManager theTileManager;
        protected ButtonManager theButtonManager;
        protected ScreenManager theScreenManager;
        protected EntityComponetManager theEntityComponetManager;
        #endregion

        #region Data
        GraphicsDeviceManager mGraphicsDeviceManager;
        Vector2 mScreenDimensions = new Vector2(736, 573);
        #endregion

        #region Construction
        public Game1()
        {
            IsMouseVisible = true;
            mGraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            mGraphicsDeviceManager.PreferredBackBufferWidth = (int)mScreenDimensions.X;
            mGraphicsDeviceManager.PreferredBackBufferHeight = (int)mScreenDimensions.Y;

           // mGraphicsDeviceManager.PreferredBackBufferWidth = (int)1;
           // mGraphicsDeviceManager.PreferredBackBufferHeight = (int)1;
        }

        protected override void Initialize()
        {
            theFileManager = FileManager.Get(this);
            theInputManager = InputManager.Get(this);
            theUtilityManager = UtilityManager.Get(this);
            theTileManager = TileManager.Get(this);
            theButtonManager = ButtonManager.Get(this);
            theScreenManager = ScreenManager.Get(this);

            theEntityComponetManager = EntityComponetManager.Get(this);
            theEntityComponetManager.Initialize();

            theScreenManager.WorldScreen = new WorldScreen();

            TileButton.Create(new FloorCopper(), Keys.E);
            TileButton.Create(new FloorMetal(), Keys.R);
            TileButton.Create(new HardWallCopper(), Keys.T);
            TileButton.Create(new HardWallMetal(), Keys.Y);
            TileButton.Create(new WallMetal(), Keys.Q);
            TileButton.Create(new CopperWall(), Keys.W);

            base.Initialize();
        }
        #endregion

        #region Methods
        protected override void Update(GameTime aGameTime)
        {
            if (theInputManager.SingleKeyPressInput(Keys.Escape))
            {
                this.Exit();
            }

            base.Update(aGameTime);

            if (theInputManager.SingleKeyPressInput(Keys.V))
            {
                theEntityComponetManager.SaveAll("Test.xml");
            }
            if (theInputManager.SingleKeyPressInput(Keys.C))
            {
                theEntityComponetManager.LoadAll("Test.xml");
            }

            if (theInputManager.SingleKeyPressInput(Keys.Space))
            {
                Vector2 mousePosition = new Vector2(theInputManager.mousePosition.X, theInputManager.mousePosition.Y);

                theButtonManager.GenerateEntity(new Vector3(theInputManager.mousePosition.X, 0, theInputManager.mousePosition.Y));
            }

            theInputManager.Update(aGameTime);
            theScreenManager.Update(aGameTime);
        }

        protected override void Draw(GameTime aGameTime)
        {
            theScreenManager.Draw(aGameTime);
        }
        #endregion
    }
}
