using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LevelEditor
{
    //<summary>
    // This will handle all drawing for the windows form.
    //</summary>

    public partial class PerspectiveControl : GraphicsDeviceControl
    {
        #region Fields
        BasicEffect effect;
        #endregion

        public readonly VertexPositionColor[] Vertices =
        {
            new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black),
            new VertexPositionColor(new Vector3( 1, -1, 0), Color.Black),
            new VertexPositionColor(new Vector3( 0,  1, 0), Color.Black),
        };

        #region Construction
        protected override void Initialize()
        {
            GameFiles.GraphicsDevice = GraphicsDevice;
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Red);

            effect = new BasicEffect(GraphicsDevice);

            Application.Idle += delegate { Invalidate(); };
        }
        #endregion

        #region Methods
        protected override void Draw()
        {
            effect.CurrentTechnique.Passes[0].Apply();
        }
        #endregion
    }
}
