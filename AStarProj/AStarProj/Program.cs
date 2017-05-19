using AStarProj.World;
using System;

namespace AStarProj
{
    class Program
    {
        static void Main(string[] args)
        {
            //build the map
            MapManager mapManager = new MapManager(10,5);
            mapManager.Setup();
            Console.WriteLine(mapManager);

            Console.ReadLine();


        }
    }
}