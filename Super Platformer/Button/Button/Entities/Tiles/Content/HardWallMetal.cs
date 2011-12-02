using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class HardWallMetal : Tile
    {
        public HardWallMetal()
        {
            IsCollidable = false;
            FilePathToGraphic = "MetalWall";
            Model = FileManager.Get().LoadModel("Monolith");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "MetalWall";
            newTile.IsCollidable = true;
            newTile.Model = FileManager.Get().LoadModel("Monolith");
            newTile.FilePathToModel = "Monolith";
        }
    }
}
