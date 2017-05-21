using System;
using System.Collections.Generic;
using System.Text;

namespace AStarProj.World
{
    class Map
    {
        GridTile[,] _mapTiles;
        int _mapSize;

        private GridTile _startLocation;
        private GridTile _endLocation;


        public GridTile StartLocation { get { return this._startLocation; } }
        public GridTile EndLocation { get { return this._endLocation; } }
        public int MapSize { get { return this._mapSize; } }
        public GridTile[,] MapTiles { get { return this._mapTiles; } }

        public Map(int mapSize)
        {
            this._mapSize = mapSize;
            this._mapTiles = new GridTile[mapSize, mapSize];
            InitMap(mapSize);
        }

        private void InitMap(int mapSize)
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    this._mapTiles[x, y] = new GridTile();
                    this._mapTiles[x, y].X = x;
                    this._mapTiles[x, y].Y = y;
                    //initially set all tiles to walkable
                    this._mapTiles[x, y].TileType = ETileTypes.Normal;
                }
            }
        }

        public void SetStartEnd(int startX, int startY, int endX, int endY)
        {
            this._startLocation = new GridTile { X = startX, Y = startY };
            this._endLocation = new GridTile { X = endX, Y = endY };

            //this._mapTiles[_startLocation.X, _startLocation.Y].IsStartEndTile = true;
            this._mapTiles[_startLocation.X, _startLocation.Y].TileType = ETileTypes.Start;
            //this._mapTiles[_endLocation.X, _endLocation.Y].IsStartEndTile = true;
            this._mapTiles[_endLocation.X, _endLocation.Y].TileType = ETileTypes.End;
        }

        public void SetWall(int x, int y)
        {
            this._mapTiles[x, y].TileType = ETileTypes.Wall;
            //this._mapTiles[x, y].Walkable = false;
        }

        public static Map CreateCopy(Map map)
        {
            Map copyMap = new Map(map.MapSize);
            for (int y = 0; y < map.MapSize; y++)
            {
                for (int x = 0; x < map.MapSize; x++)
                {
                    GridTile sourceTile = map.MapTiles[x, y];
                    copyMap.SetTile(x, y, sourceTile);
                }
            }

            copyMap.SetStartEnd(map.StartLocation.X, map.StartLocation.Y, map.EndLocation.X, map.EndLocation.Y);

            return copyMap;
        }

        private void SetTile(int x, int y, GridTile sourceTile)
        {
            this._mapTiles[x, y].TileType = sourceTile.TileType;
            
        }

        public GridTile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > this._mapSize - 1 || y > this._mapSize - 1)
            {
                return null;
            }

            return this._mapTiles[x, y];
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < this._mapSize; y++)
            {
                for (int x = 0; x < this._mapSize; x++)
                {
                    if (!this._mapTiles[x, y].IsPath)
                    {
                        switch (this._mapTiles[x, y].TileType)
                        {
                            case ETileTypes.Normal:
                                sb.Append(".");
                                break;
                            case ETileTypes.Start:
                                sb.Append("S");
                                break;
                            case ETileTypes.End:
                                sb.Append("E");
                                break;
                            case ETileTypes.Wall:
                                sb.Append("X");
                                break;
                        }
                    }
                    else
                    {
                        sb.Append("@");
                    }

                }

                sb.AppendLine();
            }

            return sb.ToString();
        }


    }



}
