using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProj.World
{

    class MapManager
    {
        private GridTile _startLocation;
        private GridTile _endLocation;
        private GridTile[,] _mapTiles;

        private int _mapSize;
        private int _obstacleCount;
        private Random _random; //classwide for a single seed, allows reproduction of maps.

        public GridTile StartLocation { get { return this._startLocation; } }
        public GridTile EndLocation { get { return this._endLocation; } }
        public int MapSize { get { return this._mapSize; } }
        public GridTile[,] MapTiles { get { return this._mapTiles; } }

        public MapManager(int mapSize, int obstacleCount, int? fixedSeed = null)
        {
            int seed = fixedSeed == null ? DateTime.Now.Millisecond : fixedSeed.Value;
            this._random = new Random(seed);

            this._obstacleCount = obstacleCount;
            this._mapSize = mapSize;
            this._mapTiles = new GridTile[mapSize, mapSize];
        }

        public void Setup()
        {
            InitMap(this._mapSize);
            InitStartEndPoints(this._mapSize);
            InitFixedObstacles(this._mapSize);
            //InitRandomObstacles(this._obstacleCount, this._mapSize);
        }

        private void InitFixedObstacles(int mapSize)
        {
            this._mapTiles[3, 2].Walkable = false;
            this._mapTiles[4, 2].Walkable = false;
            this._mapTiles[5, 2].Walkable = false;
            this._mapTiles[5, 3].Walkable = false;
            this._mapTiles[5, 4].Walkable = false;
            this._mapTiles[5, 5].Walkable = false;
            this._mapTiles[5, 6].Walkable = false;
            this._mapTiles[5, 7].Walkable = false;
            this._mapTiles[4, 7].Walkable = false;
            this._mapTiles[3, 7].Walkable = false;

        }

        private void InitRandomObstacles(int obstacleCount, int mapSize)
        {

            for (int i = 0; i < obstacleCount; i++)
            {
                // -1 as don't want obstacles on the edges
                int xLocation = PickLocation(this._mapSize);
                int yLocation = PickLocation(this._mapSize);
                this._mapTiles[xLocation, yLocation].Walkable = false;
            }
        }

        private int PickLocation(int mapSize, bool includeBorders = false)
        {
            int borderBuffer = includeBorders ? 0 : 1;
            return this._random.Next(1, this._mapSize - borderBuffer);
        }

        private void InitStartEndPoints(int mapSize)
        {
            //always start left to right, if rotateChoice is true then rotate 90deg (e.g. top to bottom)
            bool rotateChoice = this._random.Next(2) > 0;

            this._startLocation = new GridTile { X = 0, Y = 5 };
            this._endLocation = new GridTile { X = mapSize - 1, Y = 5 };

            //return this is afterwards
            //this._startLocation = new GridTile { X = 0, Y = PickLocation(mapSize, true) };
            //this._endLocation = new GridTile { X = mapSize - 1, Y = PickLocation(mapSize, true) };

            this._mapTiles[this._startLocation.X, this._startLocation.Y].IsStartEndTile = true;
            this._mapTiles[this._endLocation.X, this._endLocation.Y].IsStartEndTile = true;
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
                    this._mapTiles[x, y].Walkable = true;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < this._mapSize; y++)
            {
                for (int x = 0; x < this._mapSize; x++)
                {
                    if (this._mapTiles[x, y].Walkable)
                    {
                        if (this._mapTiles[x, y].IsStartEndTile)
                        {
                            sb.Append("@");
                            continue;
                        }

                        sb.Append(".");
                        continue;
                    }

                    sb.Append("X");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
