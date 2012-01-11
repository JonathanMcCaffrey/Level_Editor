using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    // This is Deprecated.
    public class Tile : AbstractEntity
    {
        #region Data
   
        /*private Model mModel = null;
        public Model Model
        {
            get
            {
                if (mModel == null)
                {
                    mModel = FileManager.Get().LoadModel(FilePathToModel);
                }

                return mModel;
            }
            set { mModel = value; }
        }*/

        private string mObjFilePath;
        private ObjModel mObjModel;

        public Matrix WorldMatrix
        {
            get { return Matrix.CreateTranslation(mWorldPosition.X, mWorldPosition.Y, mWorldPosition.Z); }
        }

        public Matrix ScaleMatrix
        {
            get { return Matrix.CreateScale(mScale.X, mScale.Y, mScale.Z); }
        }

        public Matrix RotationMatrix
        {
            get { return Matrix.CreateRotationX(mRotation.X) * Matrix.CreateRotationY(mRotation.Y) * Matrix.CreateRotationZ(mRotation.Z); }
        }

        private RenderTarget2D mRenderTarget;
        public RenderTarget2D RenderTarget
        {
            get { return mRenderTarget; }
            set { mRenderTarget = value; }
        }
        

        private Texture2D mColorMap = null;
        public Texture2D ColorMap
        {
            get { return mColorMap; }
            set { mColorMap = value; }
        }


        private Rectangle mSelectionRectangle = Rectangle.Empty;
        public Rectangle SelectionRectangle
        {
            get { return mSelectionRectangle; }
            set { mSelectionRectangle = value; }
        }

        #endregion

        #region Construction
        public Tile()
        {
            Initialize();
        }

        public Tile(Vector3 aCoordinate)
        {
            mWorldPosition = aCoordinate;

            theTileManager.Add(this);

            Initialize();
        }

        private void Initialize()
        {
            mManager = theTileManager;
            Name = "tile";
            mRenderTarget = new RenderTarget2D(GameFiles.GraphicsDevice, 512, 512);

            string tempFilePathToAssetDirectory = DirectoryFinder.FindContentDirectory();

            mObjFilePath = tempFilePathToAssetDirectory + "Assets\\Asteroid.obj";
            mObjModel = new ObjModel(mObjFilePath);
            ColorMap = GameFiles.LoadTexture2D("TextureEditorTest");
        }
        #endregion

        #region Methods

        float x = 0;
        static int y = 0;
        public override void Update()
        {
            x += 0.1f;

            y++;

            mWorldPosition += new Vector3((float)Math.Sin(x) * 20, (float)Math.Sin(x / 3) * 20, (float)Math.Sin(x / 2) * 40);
            mWorldPosition.X -= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 10.0f;
            mWorldPosition.Z-= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 10.0f;
            mWorldPosition.X -= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 10.0f;
            mRotation.X -= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 10.0f;
            mRotation.Z -= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 10.0f;
            mRotation.Y -= (float)Math.Sin((float)(new Random(y++).NextDouble())) * (float)(new Random(y++).NextDouble()) * 5.0f;

            mScale = new Vector3((float)Math.Sin(x / 3.0f), (float)Math.Sin(x / 5.0f), (float)Math.Sin(x / 4.0f)) + new Vector3(2.0f,2.0f,2.0f);

            if (!once)
            {
                GameFiles.GraphicsDevice.SetRenderTarget(mRenderTarget);
                GameFiles.SpriteBatch.Begin();

                GameFiles.SpriteBatch.Draw(mColorMap, Vector2.Zero, Color.White);

                GameFiles.SpriteBatch.End();
                GameFiles.GraphicsDevice.SetRenderTarget(null);
            }
        }

        bool once = false;
        public override void Draw()
        {
            mObjModel.Draw(this);

            Vector3 tempUnproject = GameFiles.GraphicsDevice.Viewport.Project(Vector3.Zero, GameFiles.ProjectionMatrix, GameFiles.ViewMatrix, WorldMatrix);

            Vector2 temp = new Vector2(tempUnproject.X, tempUnproject.Y);

            GameFiles.SpriteBatch.End();

            float scale = 2000.0f / (GameFiles.CameraPosition.X - mWorldPosition.X);

            Texture2D tempTex = GameFiles.LoadTexture2D("Selection");

            mSelectionRectangle = new Rectangle((int)temp.X, (int)temp.Y, (int)tempTex.Width, (int)tempTex.Height);

            GameFiles.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone);
            GameFiles.SpriteBatch.Draw(tempTex, temp, new Rectangle(0, 0, tempTex.Width, tempTex.Height), Color.LimeGreen, 0.0f, Vector2.Zero, scale * 0.5f, SpriteEffects.None, 0.0f);
            GameFiles.SpriteBatch.End();

            GameFiles.SpriteBatch.Begin();
        }

        public void Clone()
        {
            try
            {
                Tile clonedTile = new Tile();
                clonedTile.WorldPosition = this.WorldPosition;
                clonedTile.FilePathToGraphic = this.FilePathToGraphic;
                clonedTile.FilePathToModel = this.FilePathToModel;
                clonedTile.mObjModel = new ObjModel(this.FilePathToModel);

                theTileManager.Add(clonedTile);
            }
            catch
            {
                Console.WriteLine("{0} has an incorrect filepath of {1}. {2}.", "clonedTile", this.FilePathToModel, this.ToString());

            }
        }

        #region Common .NET Overrides
        public override string ToString()
        {
            return "EditorAssetLoader.cs";
        }
        #endregion
        #endregion
    }
}