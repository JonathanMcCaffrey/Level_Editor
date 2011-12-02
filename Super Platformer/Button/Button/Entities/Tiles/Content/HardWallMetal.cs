﻿using System;
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
            mModel = FileManager.Get().LoadModel("Monolith");
        }

        public override void Create(Vector3 aCoordinate)
        {
            Tile newTile = new Tile(aCoordinate);
            newTile.FilePathToGraphic = "MetalWall";
            newTile.IsCollidable = true;
            newTile.mModel = FileManager.Get().LoadModel("Monolith");
        }
    }
}
