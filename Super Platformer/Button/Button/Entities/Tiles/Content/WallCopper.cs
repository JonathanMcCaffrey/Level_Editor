using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class CopperWall : Tile
    {
        public CopperWall()
        {
            IsCollidable = false;
            FilePathToGraphic = "Wooden";
            Model = FileManager.Get().LoadModel("Satelite");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "Wooden";
            newTile.IsCollidable = true;
            newTile.Model = FileManager.Get().LoadModel("Satelite");
            newTile.FilePathToModel = "Satelite";
            newTile.ColorMap = FileManager.Get().LoadTexture2D("Rock");
        }
    }
}
