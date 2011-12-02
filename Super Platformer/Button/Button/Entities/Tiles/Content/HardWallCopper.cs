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
            mModel = FileManager.Get().LoadModel("Blob");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "WoodenWall";
            newTile.IsCollidable = true;
            newTile.mModel = FileManager.Get().LoadModel("Blob");
        }
    }
}
