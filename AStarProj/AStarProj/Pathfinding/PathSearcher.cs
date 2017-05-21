using AStarProj.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarProj.Pathfinding
{
    class PathSearcher
    {
        private const float SIDE_TILE_VALUE = 1.0f;
        private const float CORNER_TILE_VALUE = 1.41f;

        private List<GridTile> _openList;
        private List<GridTile> _closedList;
        private List<GridTile> _finalPath;
        //private GridTile _currentOpenTile;
        //private int _mapSize;
        //private GridTile[,] _mapTiles;
        private HeuristicCalculator _heuristicCalculator;

        private GridTile _endlocation;
        private bool _allowDiagonal;
        private Map _pathMap;
        public PathSearcher(EHeuristicTypes heuristicType, bool allowDiagonal)
        {
            this._heuristicCalculator = new HeuristicCalculator(heuristicType);
            this._allowDiagonal = allowDiagonal;
        }

        public Map SearchPath(Map map)
        {
            this._pathMap = Map.CreateCopy(map);
            //this._mapTiles = mapTiles;

            this._endlocation = this._pathMap.EndLocation;
            //this._mapSize = mapSize;
            this._openList = new List<GridTile>();
            this._closedList = new List<GridTile>();
            //this._currentOpenTile = pathmap.StartLocation;

            this._openList.Add(this._pathMap.StartLocation);

            FindPath(this._pathMap.StartLocation);
            return this._pathMap;
        }

        private void FindPath(GridTile currentTile)
        {
            GridTile endTile;
            while (this._openList.Count > 0)
            {
                currentTile = this._openList[0];
                this._closedList.Add(currentTile);
                this._openList.Remove(currentTile);

                this._openList.AddRange(GetNewTiles(currentTile));
                if ((endTile = CheckContainsEndTile(this._openList)) != null)
                {
                    this._closedList.Add(endTile);
                    CalculateFinalPath();
                    break;
                }
                this._openList.Sort((s1, s2) => s1.MovementCost.CompareTo(s2.MovementCost));
            }
        }

        private void CalculateFinalPath()
        {
            this._finalPath = new List<GridTile>();
            GridTile currentTile = this._closedList[this._closedList.Count - 1];
            while(currentTile != this._closedList[0])
            {
                if(currentTile == null)
                {
                    break;
                }
                this._finalPath.Add(currentTile);
                currentTile.IsPath = true;
                currentTile = currentTile.ParentTile;
            }
        }

        private GridTile CheckContainsEndTile(List<GridTile> openList)
        {
            foreach (GridTile gt in openList)
            {
                if (gt.X == this._endlocation.X &&
                    gt.Y == this._endlocation.Y)
                {
                    return gt;
                }
            }

            return null;
        }

        private List<GridTile> GetNewTiles(GridTile currentTile)
        {
            List<GridTile> currentTiles = new List<GridTile>();

            //add the four edges
            currentTiles.AddRange(GetEdgeTiles(currentTile));

            //add the four corners
            if (this._allowDiagonal)
            {
                currentTiles.AddRange(GetCornerTiles(currentTile));
            }
            CalculateTotalValues(currentTiles);
            return currentTiles;
        }

        private void CalculateTotalValues(List<GridTile> currentTiles)
        {
            for (int i = 0; i < currentTiles.Count; i++)
            {
                currentTiles[i].MovementCost = this._heuristicCalculator.CalculateHeuristic(currentTiles[i].X, currentTiles[i].Y, this._endlocation.X, this._endlocation.Y);
            }
        }

        private List<GridTile> GetEdgeTiles(GridTile currentTile)
        {
            List<GridTile> currentTiles = new List<GridTile>();

            AddTileIfNotNull(currentTile, currentTile.X - 1, currentTile.Y, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X + 1, currentTile.Y, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X, currentTile.Y - 1, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X, currentTile.Y + 1, currentTiles, SIDE_TILE_VALUE);

            return currentTiles;
        }

        private List<GridTile> GetCornerTiles(GridTile currentTile)
        {
            List<GridTile> currentTiles = new List<GridTile>();

            AddTileIfNotNull(currentTile, currentTile.X - 1, currentTile.Y - 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X - 1, currentTile.Y + 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X + 1, currentTile.Y - 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile, currentTile.X + 1, currentTile.Y + 1, currentTiles, CORNER_TILE_VALUE);

            return currentTiles;
        }

        private void AddTileIfNotNull(GridTile parentTile, int x, int y, List<GridTile> listToAddTo, float tileValue)
        {
            GridTile newTile = this._pathMap.GetTile(x, y);
            if (newTile != null)
            {
                if (this._openList.Contains(newTile) || this._closedList.Contains(newTile) || newTile.TileType == ETileTypes.Wall)
                {
                    return;
                }
                newTile.ParentTile = parentTile;
                newTile.MovementCost = tileValue;
                listToAddTo.Add(newTile);
            }
        }


        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int y = 0; y < this._pathMap.MapSize; y++)
        //    {
        //        for (int x = 0; x < this._pathMap.MapSize; x++)
        //        {
        //            bool isPath = false;
        //            foreach(GridTile g in this._finalPath)
        //            {
        //                if (g.X == x && g.Y == y)
        //                {
        //                    isPath = true;
        //                }
        //            }

        //            sb.Append(isPath ? "#" : "~");
        //        }

        //        sb.AppendLine();
        //    }

        //    return sb.ToString();
        //}

    }
}
