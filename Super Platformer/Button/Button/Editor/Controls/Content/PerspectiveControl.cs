using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace LevelEditor
{
    //<summary>
    // This will handle all drawing for the windows form.
    //</summary>

    public partial class PerspectiveControl : GraphicsDeviceControl
    {
        #region Fields
        BasicEffect effect;
        WorldBox temp;
        Stopwatch timer;
        SpriteBatch spriteBatch;
        #endregion


        public readonly VertexPositionColor[] Vertices =
        {
            new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red),
            new VertexPositionColor(new Vector3( 1, -1, 0), Color.Green),
            new VertexPositionColor(new Vector3( 0,  1, 0), Color.Black),
        };

        #region Construction
        protected override void Initialize()
        {
         //   GameFiles.GraphicsDevice = GraphicsDevice;
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Red);

            temp = new WorldBox();

            timer = Stopwatch.StartNew();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;

            //GameFiles.BasicEffect = effect;

            Application.Idle += delegate { Invalidate(); };
        }
        #endregion

        #region Methods
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);
            
            effect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                                             Vertices, 0, 1);

            spriteBatch.Draw(GameFiles.LoadTexture2D("TextureEditorTest"), Vector2.Zero, Color.White);

            spriteBatch.End();
        }
        #endregion
    }
}
