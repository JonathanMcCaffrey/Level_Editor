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
using System.Windows.Forms;

#region Program
namespace LevelEditor
{
#if WINDOWS || XBOX

    static class TheGame
    {
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();

          //  LevelEditorInterface editor = new LevelEditorInterface();

            Game1 game2 = new Game1();

            game2.Run();
            
            /*
            using (Game1 game = new Game1())
            {
                game.Run();
            }*/

            return 0;
        }
    }
#endif
}
#endregion

namespace LevelEditor
{
    // This is Deprecated.
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Singletons
        protected TileManager theTileManager;
        protected ScreenManager theScreenManager;
        protected EntityComponetManager theEntityComponetManager;
        #endregion

        #region Data
        GraphicsDeviceManager mGraphicsDeviceManager;
        Vector2 mScreenDimensions = new Vector2(736, 573);
        IntPtr mWindowHandle;
        #endregion

        #region Construction
        public Game1()
        {
            IsMouseVisible = true;
            mGraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            mGraphicsDeviceManager.PreferredBackBufferWidth = (int)mScreenDimensions.X;
            mGraphicsDeviceManager.PreferredBackBufferHeight = (int)mScreenDimensions.Y;

            DirectoryFinder.FindProjectDirectory();
        }

        public Game1(IntPtr a_WindowHandle)
        {
            mWindowHandle = a_WindowHandle;

            IsMouseVisible = true;
            mGraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            mGraphicsDeviceManager.PreferredBackBufferWidth = (int)mScreenDimensions.X;
            mGraphicsDeviceManager.PreferredBackBufferHeight = (int)mScreenDimensions.Y;

            DirectoryFinder.FindProjectDirectory();
        }

        protected override void Initialize()
        {

      /*      if (mWindowHandle != null)
            {
                GraphicsDevice.PresentationParameters.DeviceWindowHandle = mWindowHandle;
                GraphicsDevice.Present(null,null,mWindowHandle);
            }*/

            GameFiles.StartGameFileManager(this);

            theTileManager = TileManager.Get(this);
            theScreenManager = ScreenManager.Get(this);

            theEntityComponetManager = EntityComponetManager.Get(this);
            theEntityComponetManager.Initialize();

            theScreenManager.WorldScreen = new WorldScreen();

            base.Initialize();
        }
        #endregion

        #region Methods
        protected override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);

            theScreenManager.Update(aGameTime);
        }

        protected override void Draw(GameTime aGameTime)
        {
            theScreenManager.Draw(aGameTime);
        }
        #endregion
    }
}