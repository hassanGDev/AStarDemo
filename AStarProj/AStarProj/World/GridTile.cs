using System;
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
        public bool Walkable;
        public float MovementCost;
    }

    
}
