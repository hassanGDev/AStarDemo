using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarProj.World
{

    class MapManager
    {
        private int _mapSize;
        private int _obstacleCount;
        private Random _random; //classwide for a single seed, allows reproduction of maps.


        //Map _map;

        public MapManager(int mapSize, int obstacleCount, int? fixedSeed = null)
        {
            int seed = fixedSeed == null ? DateTime.Now.Millisecond : fixedSeed.Value;
            this._random = new Random(seed);

            this._obstacleCount = obstacleCount;


            this._mapSize = mapSize;
            //this._mapTiles = new GridTile[mapSize, mapSize];
        }

        public Map Create()
        {
            
            Map map = new Map(this._mapSize);
            InitStartEndPoints(map);
            InitFixedObstacles_Debug(map);
            return map;
        }

        private void InitFixedObstacles_Debug(Map map)
        {
            map.SetWall(3, 2);
            map.SetWall(3, 2);
            map.SetWall(4, 2);
            map.SetWall(5, 2);
            map.SetWall(5, 3);
            map.SetWall(5, 4);
            map.SetWall(5, 5);
            map.SetWall(5, 6);
            map.SetWall(5, 7);
            map.SetWall(4, 7);
            map.SetWall(3, 7);
        }

        private void InitRandomObstacles(Map map, int obstacleCount)
        {

            for (int i = 0; i < obstacleCount; i++)
            {
                // -1 as don't want obstacles on the edges
                int xLocation = PickRandomLocation(map.MapSize);
                int yLocation = PickRandomLocation(map.MapSize);
                map.SetWall(xLocation, yLocation);
            }
        }

        private int PickRandomLocation(int mapSize, bool includeBorders = false)
        {
            int borderBuffer = includeBorders ? 0 : 1;
            return this._random.Next(1, mapSize - borderBuffer);
        }

        private void InitStartEndPoints(Map map)
        {
            //always start left to right, if rotateChoice is true then rotate 90deg (e.g. top to bottom)
            bool rotateChoice = this._random.Next(2) > 0;

            map.SetStartEnd(0, PickRandomLocation(map.MapSize, true), map.MapSize - 1, PickRandomLocation(map.MapSize, true));
        }


    }


}


