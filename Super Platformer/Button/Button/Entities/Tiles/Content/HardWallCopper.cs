using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class HardWallCopper : Tile
    {
        public HardWallCopper()
        {
            IsCollidable = false;
            FilePathToGraphic = "WoodenWall";
            Model = FileManager.Get().LoadModel("Blob");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "WoodenWall";
            newTile.IsCollidable = true;
            newTile.Model = FileManager.Get().LoadModel("Blob");
            newTile.FilePathToModel = "Blob";
            newTile.ColorMap = FileManager.Get().LoadTexture2D("Blueness");
        }
    }
}
