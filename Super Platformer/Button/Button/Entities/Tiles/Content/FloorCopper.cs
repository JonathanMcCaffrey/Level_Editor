using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class FloorCopper : Tile
    {
        public FloorCopper()
        {
            IsCollidable = false;
            FilePathToGraphic = "WoodenFloor";
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "WoodenFloor";
            newTile.IsCollidable = false;
        }
    }
}
