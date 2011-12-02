using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class FloorMetal : Tile
    {
        public FloorMetal()
        {
            IsCollidable = false;
            FilePathToGraphic = "MetalFloor";
            Model = FileManager.Get().LoadModel("Asteroid");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "MetalFloor";
            newTile.IsCollidable = false;
            newTile.Model = FileManager.Get().LoadModel("Asteroid");
            newTile.FilePathToModel = "Asteroid";
        }
    }
}
