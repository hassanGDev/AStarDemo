﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProj.World
{
    public class GridTile
    {
        public GridTile ParentTile;

        public int X;
        public int Y;

        public float MovementCost;

        //public bool Walkable;
        //public bool IsStartEndTile;
        public bool IsPath;

        public ETileTypes TileType;
    }

    
}
