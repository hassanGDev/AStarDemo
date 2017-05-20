﻿using AStarProj.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarProj.Pathfinding
{
    class PathSearcher
    {
        private const float SIDE_TILE_VALUE = 1.0f;
        private const float CORNER_TILE_VALUE = 1.4f;

        private List<GridTile> _openList;
        private List<GridTile> _closedList;
        private GridTile _currentOpenTile;
        private int _mapSize;
        private GridTile[,] _mapTiles;
        private HeuristicCalculator _heuristicCalculator;

        private GridTile _endlocation;

        public PathSearcher(HeuristicTypes heuristicType)
        {
            this._heuristicCalculator = new HeuristicCalculator(heuristicType);
        }

        public void SearchPath(GridTile startLocation, GridTile endLocation, GridTile[,] mapTiles, int mapSize)
        {
            this._mapTiles = mapTiles;

            this._endlocation = endLocation;
            this._mapSize = mapSize;
            this._openList = new List<GridTile>();
            this._closedList = new List<GridTile>();
            this._currentOpenTile = startLocation;
            
            this._openList.Add(startLocation);
\

            FindPath(startLocation);
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
                if((endTile = CheckContainsEndTile(this._openList)) != null)
                {
                    this._closedList.Add(endTile);
                    break;
                }
                this._openList.Sort((s1, s2) => s1.MovementCost.CompareTo(s2.MovementCost));
            }
        }

        private GridTile CheckContainsEndTile(List<GridTile> openList)
        {
            foreach(GridTile gt in openList)
            {
                if(gt.X == this._endlocation.X &&
                    gt.Y == this._endlocation.Y )
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
            currentTiles.AddRange(GetFourWayMovementTiles(currentTile));

            //add the four corners
            //currentTiles.AddRange(GetFourWayMovementTiles(currentTile));
            CalculateTotalValues(currentTiles);
            return currentTiles;
        }

        private void CalculateTotalValues(List<GridTile> currentTiles)
        {
            for(int i = 0; i < currentTiles.Count; i++)
            {
                currentTiles[i].MovementCost = this._heuristicCalculator.CalculateHeuristic(currentTiles[i].X, currentTiles[i].Y,this._endlocation.X, this._endlocation.Y);
            }
        }

        private List<GridTile> GetFourWayMovementTiles(GridTile currentTile)
        {
            List<GridTile> currentTiles = new List<GridTile>();

            AddTileIfNotNull(currentTile.X - 1, currentTile.Y, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile.X + 1, currentTile.Y, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile.X, currentTile.Y - 1, currentTiles, SIDE_TILE_VALUE);
            AddTileIfNotNull(currentTile.X, currentTile.Y + 1, currentTiles, SIDE_TILE_VALUE);

            return currentTiles;
        }

        private List<GridTile> GetEightWayMovementTiles(GridTile currentTile)
        {
            List<GridTile> currentTiles = new List<GridTile>();

            AddTileIfNotNull(currentTile.X - 1, currentTile.Y - 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile.X - 1, currentTile.Y + 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile.X + 1, currentTile.Y - 1, currentTiles, CORNER_TILE_VALUE);
            AddTileIfNotNull(currentTile.X + 1, currentTile.Y + 1, currentTiles, CORNER_TILE_VALUE);

            return currentTiles;
        }

        private void AddTileIfNotNull(int x, int y, List<GridTile> listToAddTo, float tileValue)
        {
            GridTile edgeTile = GetTile(x, y);
            if (edgeTile != null)
            {
                edgeTile.MovementCost = tileValue;
                listToAddTo.Add(edgeTile);
            }
        }

        private GridTile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > this._mapSize -1 || y > this._mapSize -1 )
            {
                return null;
            }

            return this._mapTiles[x, y];
        }

    }
}
