﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Button
{
    public class WallMetal : Tile
    {
        public WallMetal()
        {
            IsCollidable = false;
            FilePathToGraphic = "Metal";
            Model = FileManager.Get().LoadModel("Spaceship");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "Metal";
            newTile.IsCollidable = true;
            newTile.Model = FileManager.Get().LoadModel("Spaceship");
            newTile.FilePathToModel = "Spaceship";
        }
    }
}
